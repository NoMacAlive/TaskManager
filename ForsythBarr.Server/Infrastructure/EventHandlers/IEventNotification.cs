using MediatR;

namespace ForsythBarr.Server.Infrastructure.EventHandlers;

interface IEventNotification : INotification
{
    void SendNotification();
}