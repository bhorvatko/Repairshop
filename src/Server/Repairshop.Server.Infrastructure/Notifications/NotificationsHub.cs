using Microsoft.AspNetCore.SignalR;
using Repairshop.Shared.Common.Notifications;

namespace Repairshop.Server.Infrastructure.Notifications;

internal class NotificationsHub
    : Hub
{
    public async Task SendNotification(
        object notification,
        CancellationToken cancellationToken)
    {
        await Clients.All.SendAsync(
            NotificationConstants.ReceiveNotificationMethodName, 
            notification, 
            cancellationToken);
    }
}
