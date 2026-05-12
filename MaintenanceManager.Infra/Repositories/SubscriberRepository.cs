using MaintenanceManager.Core.Models;
using MaintenanceManager.Core.Repositories.Abstractions;

namespace MaintenanceManager.Infra.Repositories;

public class SubscriberRepository : ISubscriberRespository
{
    public Task<IEnumerable<Subscriber>> GetSubscribersAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Subscriber> subscribers = [
            new() { Name = "John Doe", Email = "john@doe.com" },
            new() { Name = "Jane Doe", Email = "jane@doe.com" }
        ];

        return System.Threading.Tasks.Task.FromResult(subscribers);
    }
}
