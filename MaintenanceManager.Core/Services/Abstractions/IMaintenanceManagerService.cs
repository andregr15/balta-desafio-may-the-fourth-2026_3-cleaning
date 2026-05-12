namespace MaintenanceManager.Core.Services.Abstractions;

public interface IMaintenanceManagerService
{
    Task SendTasksAsync(CancellationToken cancellationToken = default);
}
