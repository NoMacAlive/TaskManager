namespace ForsythBarr.Server.Domain.Models;

public enum TaskStatus
{
    Pending,
    InProgress,
    Completed
}

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }
    public TaskStatus Status { get; set; }
}