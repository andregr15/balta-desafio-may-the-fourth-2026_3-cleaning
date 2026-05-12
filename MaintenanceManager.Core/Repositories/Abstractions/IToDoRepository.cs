using MaintenanceManager.Core.Models;

namespace MaintenanceManager.Core.Repositories.Abstractions;

public interface IToDoRepository
{
    Task<ToDo> GetToDoAsync(DateOnly date, CancellationToken cancellationToken = default);
}
