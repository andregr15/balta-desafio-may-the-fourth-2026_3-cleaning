using MaintenanceManager.Ai.Agents;
using MaintenanceManager.Ai.Providers;
using MaintenanceManager.Core.Enums;
using MaintenanceManager.Core.Models;
using MaintenanceManager.Core.Providers.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MaintenanceManager.Ai;

public static class DependencyInjection
{
    public static IServiceCollection AddAgents(this IServiceCollection services)
    {
        services.AddKeyedTransient<IPromptProvider, FilePromptProvider>(PromptProviderType.File);
        services.AddTransient<IAgent<ToDo, IEnumerable<Subscriber>>, MaintenanceManagerAgent>();

        return services;
    }
}
