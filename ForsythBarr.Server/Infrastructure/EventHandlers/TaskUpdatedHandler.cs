using MediatR;
using Microsoft.OpenApi.Extensions;
using TaskStatus = ForsythBarr.Server.Domain.Models.TaskStatus;

namespace ForsythBarr.Server.Infrastructure.EventHandlers;

public enum UpdateType
{
    STATUS,
    DUEDATE,
    PRIORITY
}
public class TaskUpdatedEvent : IEventNotification
{
    public int Id { get; set; }
    public int UserId { get; set; }
    
    public void SendNotification()
    {
        Console.WriteLine($"Task created with Id: {Id} by user with Id: {UserId}");
    }
}

public class TaskStatusUpdatedEvent : TaskUpdatedEvent
{
    public TaskStatus OldStatus { get; set; }
    public TaskStatus NewStatus { get; set; }
    
    public new void SendNotification()
    {
        Console.WriteLine($"Task status updated with Id: {Id} by user with Id: {UserId}, old status: {OldStatus.GetDisplayName()} to new status: {NewStatus.GetDisplayName()}");
    }
}

public class TaskDueDateUpdatedEvent : TaskUpdatedEvent
{
    public DateTime OldDueDate { get; set; }
    public DateTime NewDueDate { get; set; }
    
    public new void SendNotification()
    {
        Console.WriteLine($"Task due date updated with Id: {Id} by user with Id: {UserId}, old due date: {OldDueDate} to new due date: {NewDueDate}");
    }
}

public class TaskPriorityUpdatedEvent : TaskUpdatedEvent
{
    public int OldPriority { get; set; }
    public int NewPriority { get; set; }
    
    public new void SendNotification()
    {
        Console.WriteLine($"Task priority updated with Id: {Id} by user with Id: {UserId}, old priority: {OldPriority} to new priority: {NewPriority}");
    }
}

public class TaskUpdatedHandler : INotificationHandler<TaskUpdatedEvent>
{
    public Task Handle(TaskUpdatedEvent notification, CancellationToken cancellationToken)
    {
        notification.SendNotification();
        return Task.CompletedTask;
    }
}