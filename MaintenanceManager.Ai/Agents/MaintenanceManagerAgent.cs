using MaintenanceManager.Ai.Models;
using MaintenanceManager.Core;
using MaintenanceManager.Core.Enums;
using MaintenanceManager.Core.Models;
using MaintenanceManager.Core.Providers.Abstractions;
using MaintenanceManager.Core.Repositories.Abstractions;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenAI;
using OpenAI.Chat;
using System.Text.Json;

namespace MaintenanceManager.Ai.Agents;

public class MaintenanceManagerAgent(
    ILogger<MaintenanceManagerAgent> logger,
    ISubscriberRespository subscriberRespository,
    [FromKeyedServices(PromptProviderType.File)] IPromptProvider promptProvider
) : IAgent<ToDo, IEnumerable<Subscriber>>
{
    private const float Temperature = 0.7f;
    private const string Prompt = "Distributa as tarefas de manutenção de forma eficiente se basenando no json de tarefas: ";

    public async Task<IEnumerable<Subscriber>> ExecuteAsync(ToDo data, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Executing MaintenanceManagerAgent with {ToDoCount} tasks", data.Tasks.Count);

        var client = new OpenAIClient(Configuration.OpenApiKey);
        var instructions = await promptProvider.GetPromptAsync(nameof(MaintenanceManagerAgent));

        var agent = client
            .GetChatClient(AiModels.Gpt4Omini)
            .AsAIAgent(new ChatClientAgentOptions
            {
                Name = nameof(MaintenanceManagerAgent),
                Description = "Agente responsavel por distribuir tarefas de manutenção de forma eficiente",
                ChatOptions = new ChatOptions
                {
                    Temperature = Temperature,
                    Instructions = instructions
                }
            });

        var prompt = $"{Prompt} {JsonSerializer.Serialize(data)}";
        var subscribers = await subscriberRespository.GetSubscribersAsync(cancellationToken);

        prompt = $"{prompt} pessoas para distribuir as tarefas: {JsonSerializer.Serialize(subscribers)}";

        logger.LogInformation("Sending prompt to AI: {Prompt}", prompt);

        var response = await agent.RunAsync<IEnumerable<Subscriber>>(prompt, cancellationToken: cancellationToken);

        logger.LogInformation("Received response from AI: {Response}", response.Result);

        return response.Result;
    }
}
