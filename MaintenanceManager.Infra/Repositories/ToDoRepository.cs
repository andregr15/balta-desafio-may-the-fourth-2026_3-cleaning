using MaintenanceManager.Core.Models;
using MaintenanceManager.Core.Repositories.Abstractions;

namespace MaintenanceManager.Infra.Repositories;

public class ToDoRepository : IToDoRepository
{
    public async Task<ToDo> GetToDoAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        List<ToDo> toDos = [
            new () {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Tasks = [
                    new Core.Models.Task { Description = "Check the air filters." },
                    new Core.Models.Task { Description = "Lubricate the conveyor belts." }
                ]
            },
            new () {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Tasks = [
                    new Core.Models.Task { Description = "Inspect the cooling system." },
                    new Core.Models.Task { Description = "Test the backup generator." }
                ]
            },
            new () {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                Tasks = [
                    new Core.Models.Task { Description = "Clean the ventilation ducts." },
                    new Core.Models.Task { Description = "Check the fire suppression system." }
                ]
            }
        ];

        return toDos.FirstOrDefault(t => t.Date == date)
            ?? await System.Threading.Tasks.Task.FromResult(new ToDo { Date = date });
    }
}
