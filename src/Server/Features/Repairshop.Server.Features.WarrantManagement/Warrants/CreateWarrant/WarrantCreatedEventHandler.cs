using Repairshop.Server.Common.Notifications;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.CreateWarrant;
internal class WarrantCreatedEventHandler
    : DomainEventHandler<WarrantCreatedEvent, WarrantCreatedNotification>
{
    public WarrantCreatedEventHandler(INotificationDispatcher notificationDispatcher) 
        : base(notificationDispatcher)
    {
    }

    public override WarrantCreatedNotification CreateNotification(WarrantCreatedEvent domainEvent)
    {
        WarrantModel warrantModel = domainEvent.Warrant.ToWarrantModel();

        return new WarrantCreatedNotification() { Warrant = warrantModel };
    }
}
