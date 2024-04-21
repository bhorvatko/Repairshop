using Microsoft.AspNetCore.SignalR;

namespace Repairshop.Server.Infrastructure.Notifications;

internal class NotificationsHub
    : Hub
{
    public async Task SendNotification(
        object notification,
        CancellationToken cancellationToken)
    {
        await Clients.All.SendAsync(
            notification.GetType().Name, 
            notification, 
            cancellationToken);
    }
}
