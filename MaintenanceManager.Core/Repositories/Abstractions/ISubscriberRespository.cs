using MaintenanceManager.Core.Models;

namespace MaintenanceManager.Core.Repositories.Abstractions;

public interface ISubscriberRespository
{
    Task<IEnumerable<Subscriber>> GetSubscribersAsync(CancellationToken cancellationToken = default);
}
