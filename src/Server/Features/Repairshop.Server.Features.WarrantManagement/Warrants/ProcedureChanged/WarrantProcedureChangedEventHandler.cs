using Repairshop.Server.Common.Notifications;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.ProcedureChanged;

internal class WarrantProcedureChangedEventHandler
    : DomainEventHandler<WarrantProcedureChangedEvent, WarrantProcedureChangedNotification>
{
    public WarrantProcedureChangedEventHandler(INotificationDispatcher notificationDispatcher) 
        : base(notificationDispatcher)
    {
    }

    public override WarrantProcedureChangedNotification CreateNotification(WarrantProcedureChangedEvent domainEvent)
    {
        return new WarrantProcedureChangedNotification()
        {
            Warrant = domainEvent.Warrant.ToWarrantModel()
        }; 
    }
}
