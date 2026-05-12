namespace MaintenanceManager.Core.Models;

public class ToDo
{
    public DateOnly Date { get; set; }
    public List<Task> Tasks { get; set; } = [];
}
