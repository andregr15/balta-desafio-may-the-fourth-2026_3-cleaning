using MaintenanceManager.Core.Enums;
using MaintenanceManager.Core.Models;
using MaintenanceManager.Core.Repositories.Abstractions;
using MaintenanceManager.Core.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MaintenanceManager.Infra.Services;

public class MaintenanceManagerService(
    ILogger<MaintenanceManagerService> logger,
    IToDoRepository toDoRepository,
    IAgent<ToDo, IEnumerable<Subscriber>> maintenanceManagerAgent,
    [FromKeyedServices(NotificationType.Email)] INotificationService emailNotificationService
) : IMaintenanceManagerService
{
    public async System.Threading.Tasks.Task SendTasksAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting next maintenance task");

        var toDos = await toDoRepository.GetToDoAsync(DateOnly.FromDateTime(DateTime.Today), cancellationToken);

        var subscribers = await maintenanceManagerAgent.ExecuteAsync(toDos, cancellationToken);

        foreach (var subscriber in subscribers)
        {
            var message = $"Olá {subscriber.Name}, você tem uma nova tarefa de manutenção atribuída. Por favor, verifique o sistema para mais detalhes.";
            await emailNotificationService
                .SendNotificationAsync(
                    subscriber.Name,
                    subscriber.Email,
                    "Nova Tarefa de Manutenção",
                    message,
                    cancellationToken
                );
        }
    }
}
