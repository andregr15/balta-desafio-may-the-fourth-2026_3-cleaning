using MaintenanceManager.Core.Enums;
using MaintenanceManager.Core.Repositories.Abstractions;
using MaintenanceManager.Core.Services.Abstractions;
using MaintenanceManager.Infra.Repositories;
using MaintenanceManager.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MaintenanceManager.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddTransient<ISubscriberRespository, SubscriberRepository>();
        services.AddTransient<IToDoRepository, ToDoRepository>();

        services.AddKeyedTransient<INotificationService, EmailNotificationService>(NotificationType.Email);
        services.AddTransient<IMaintenanceManagerService, MaintenanceManagerService>();

        return services;
    }
}