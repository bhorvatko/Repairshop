using Repairshop.Server.Common.Notifications;
using Repairshop.Shared.Features.WarrantManagement.Technicians;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;

internal class WarrantAssignedEventHandler
    : DomainEventHandler<WarrantAssignedEvent, WarrantAssignedNotification>
{
    public WarrantAssignedEventHandler(INotificationDispatcher notificationDispatcher) 
        : base(notificationDispatcher)
    {
    }

    public override WarrantAssignedNotification CreateNotification(WarrantAssignedEvent domainEvent) =>
        new()
        {
            TechnicianId = domainEvent.TechnicianId,
            WarrantId = domainEvent.WarrantId
        };
}
