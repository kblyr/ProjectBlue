using MediatR;

namespace JIL.Handlers;

public abstract class NotificationBusPublishHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : class, INotification
{
    readonly IBusAdapter _bus;

    protected NotificationBusPublishHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public Task Handle(TEvent notification, CancellationToken cancellationToken)
    {
        return _bus.Publish(notification, cancellationToken);
    }
}