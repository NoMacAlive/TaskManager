using MediatR;

namespace ForsythBarr.Server.Infrastructure.EventHandlers;

public class TaskCreatedEvent : IEventNotification
{
    public int Id { get; set; }
    public int UserId { get; set; }
    
    public void SendNotification()
    {
        Console.WriteLine($"Task created with Id: {Id} by user with Id: {UserId}");
    }
}
public class TaskCreatedHandler : INotificationHandler<TaskCreatedEvent>
{
    public Task Handle(TaskCreatedEvent notification, CancellationToken cancellationToken)
    {
        notification.SendNotification();
        return Task.CompletedTask;
    }
}