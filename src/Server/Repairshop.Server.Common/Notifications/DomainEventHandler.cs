using MediatR;

namespace Repairshop.Server.Common.Notifications;

public abstract class DomainEventHandler<TDomainEvent, TNotification>
    : INotificationHandler<TDomainEvent>
    where TDomainEvent : INotification
    where TNotification : notnull
{
    private readonly INotificationDispatcher _notificationDispatcher;

    public DomainEventHandler(INotificationDispatcher notificationDispatcher)
    {
        _notificationDispatcher = notificationDispatcher;
    }

    public async Task Handle(
        TDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        TNotification notification = CreateNotification(domainEvent);

        await _notificationDispatcher.DispatchNotification(
            notification,
            cancellationToken);
    }

    public abstract TNotification CreateNotification(TDomainEvent domainEvent);
}
