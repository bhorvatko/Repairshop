using Microsoft.AspNetCore.SignalR;
using Repairshop.Server.Common.Notifications;

namespace Repairshop.Server.Infrastructure.Notifications;
internal class NotificationDispatcher
    : INotificationDispatcher
{
    private readonly IHubContext<NotificationsHub> _hubContext;
    private readonly HubConnectionIdProvider _hubConnectionIdProvider;

    public NotificationDispatcher(
        IHubContext<NotificationsHub> hubContext,
        HubConnectionIdProvider hubConnectionIdProvider)
    {
        _hubContext = hubContext;
        _hubConnectionIdProvider = hubConnectionIdProvider;
    }

    public async Task DispatchNotification(
        object notification,
        CancellationToken cancellationToken)
    {
        string? hubConnectionId = _hubConnectionIdProvider.GethubConnectionId();

        IEnumerable<string> excludedConnectionIds =
            hubConnectionId is null 
                ? Enumerable.Empty<string>() 
                : new[] { hubConnectionId };

        await _hubContext
            .Clients
            .AllExcept(excludedConnectionIds)
            .SendAsync(
                notification.GetType().Name, 
                notification, 
                cancellationToken);
    }
}
