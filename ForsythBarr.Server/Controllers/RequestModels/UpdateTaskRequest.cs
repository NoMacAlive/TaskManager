using TaskStatus = ForsythBarr.Server.Domain.Models.TaskStatus;

namespace ForsythBarr.Server.Controllers.RequestModels;

public class UpdateTaskRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }
    public TaskStatus Status { get; set; }
}