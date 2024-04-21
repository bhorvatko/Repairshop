using Microsoft.AspNetCore.SignalR;
using Repairshop.Server.Common.Notifications;
using Repairshop.Shared.Common.Notifications;

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
            NotificationConstants.ReceiveNotificationMethodName, 
            notification, 
            cancellationToken);
    }
}
