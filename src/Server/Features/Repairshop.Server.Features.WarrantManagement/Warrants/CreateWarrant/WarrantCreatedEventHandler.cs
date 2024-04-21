using Repairshop.Server.Common.Notifications;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
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
        Warrant warrantEntity = domainEvent.Warrant;

        Procedure procedure = warrantEntity.CurrentStep!.Procedure;
        WarrantStep? currentStep = warrantEntity.CurrentStep;
        WarrantStepTransition? nextTransition = currentStep?.NextTransition;
        WarrantStepTransition? previousTransition = currentStep?.PreviousTransition;

        WarrantModel warrantModel = new WarrantModel()
        {
            Id = warrantEntity.Id,
            Deadline = warrantEntity.Deadline,
            IsUrgent = warrantEntity.IsUrgent,
            Title = warrantEntity.Title,
            TechnicianId = warrantEntity.TechnicianId,
            Procedure = new ProcedureModel()
            {
                Id = procedure.Id,
                Color = procedure.Color,
                Name = procedure.Name,
            },
            CanBeAdvancedByFrontOffice = nextTransition?.CanBePerformedByFrontOffice == true,
            CanBeAdvancedByWorkshop = nextTransition?.CanBePerformedByWorkshop == true,
            CanBeRolledBackByFrontOffice = previousTransition?.CanBePerformedByFrontOffice == true,
            CanBeRolledBackByWorkshop = previousTransition?.CanBePerformedByWorkshop == true,
            NextStepId = currentStep?.NextStep?.Id,
            PreviousStepId = currentStep?.PreviousStep?.Id,
        };

        return new WarrantCreatedNotification() { WarrantModel = warrantModel };
    }
}
