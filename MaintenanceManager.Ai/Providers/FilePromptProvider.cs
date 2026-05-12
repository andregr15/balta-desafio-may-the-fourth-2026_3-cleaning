using MaintenanceManager.Core.Providers.Abstractions;

namespace MaintenanceManager.Ai.Providers;

public class FilePromptProvider : IPromptProvider
{
    public async Task<string> GetPromptAsync(string agentName, CancellationToken cancellationToken = default)
    {
        var assembly = typeof(FilePromptProvider).Assembly;
        var resourceName = $"MaintenanceManager.Ai.Prompts.{agentName}.md";

        using var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Resource '{resourceName}' not found.");

        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync(cancellationToken);
    }
}
