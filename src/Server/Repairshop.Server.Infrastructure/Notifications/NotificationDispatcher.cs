using Microsoft.AspNetCore.SignalR;
using Repairshop.Server.Common.Notifications;

namespace Repairshop.Server.Infrastructure.Notifications;
internal class NotificationDispatcher
    : INotificationDispatcher
{
    private readonly IHubContext<NotificationsHub> _hubContext;

    public NotificationDispatcher(IHubContext<NotificationsHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task DispatchNotification(
        object notification,
        CancellationToken cancellationToken)
    {
        await _hubContext.Clients.All.SendAsync(
            notification.GetType().Name, 
            notification, 
            cancellationToken);
    }
}
