namespace MaintenanceManager.Core.Providers.Abstractions;

public interface IPromptProvider
{
    Task<string> GetPromptAsync(string agentName, CancellationToken cancellationToken = default);
}
