namespace Repairshop.Server.Common.Notifications;

public interface INotificationDispatcher
{
    Task DispatchNotification(
        object notification,
        CancellationToken cancellationToken);
}
