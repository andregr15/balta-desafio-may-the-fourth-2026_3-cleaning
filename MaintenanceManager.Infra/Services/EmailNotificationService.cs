using MaintenanceManager.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace MaintenanceManager.Infra.Services;

public class EmailNotificationService(ILogger<EmailNotificationService> logger) : INotificationService
{
    public async Task SendNotificationAsync(
        string toName,
        string to,
        string subject,
        string body,
        CancellationToken cancellationToken = default
    )
    {
        await Task.Delay(1500, cancellationToken); // Simulate email sending delay
        logger.LogInformation("Email sent to {ToName} ({to}) with subject '{Subject}'", toName, to, subject);
    }
}
