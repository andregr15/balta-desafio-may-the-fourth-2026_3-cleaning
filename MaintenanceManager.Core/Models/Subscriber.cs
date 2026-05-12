namespace MaintenanceManager.Core.Models;

public class Subscriber
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public Task TaskToDo { get; set; } = new();
}
