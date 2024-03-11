using MediatR;
using Repairshop.Server.Common.Notifications;
using Repairshop.Shared.Features.WarrantManagement.Technicians;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;

internal class WarrantAssignedEventHandler
    : INotificationHandler<WarrantAssignedEvent>
{
    private readonly INotificationDispatcher _notificationDispatcher;

    public WarrantAssignedEventHandler(INotificationDispatcher notificationDispatcher)
    {
        _notificationDispatcher = notificationDispatcher;
    }

    public async Task Handle(
        WarrantAssignedEvent notification, 
        CancellationToken cancellationToken)
    {
        WarrantAssignedNotification warrantAssignedNotification = new()
        {
            TechnicianId = notification.TechnicianId,
            WarrantId = notification.WarrantId
        };

        await _notificationDispatcher.DispatchNotification(
            warrantAssignedNotification,
            cancellationToken);
    }
}
