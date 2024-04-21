using Repairshop.Server.Common.Notifications;
using Repairshop.Server.Features.WarrantManagement.Warrants;
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
            ToTechnicianId = domainEvent.ToTechnicianId,
            FromTechnicianId = domainEvent.FromTechnicianId,
            Warrant = domainEvent.Warrant.ToWarrantModel()
        };
}
