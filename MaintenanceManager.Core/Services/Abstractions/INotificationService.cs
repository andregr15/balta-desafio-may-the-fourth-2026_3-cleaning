namespace MaintenanceManager.Core.Services.Abstractions;

public interface INotificationService
{
    Task SendNotificationAsync(string toName, string to, string subject, string body, CancellationToken cancellationToken = default);
}
