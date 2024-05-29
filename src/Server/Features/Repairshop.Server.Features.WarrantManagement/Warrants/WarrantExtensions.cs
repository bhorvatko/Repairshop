using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants;

internal static class WarrantExtensions
{
    public static WarrantModel ToWarrantModel(this Warrant warrantEntity)
    {
        Procedure procedure = warrantEntity.CurrentStep.Procedure;
        WarrantStep? currentStep = warrantEntity.CurrentStep;
        WarrantStepTransition? nextTransition = currentStep?.NextTransition;
        WarrantStepTransition? previousTransition = currentStep?.PreviousTransition;

        WarrantModel warrantModel = new WarrantModel()
        {
            Id = warrantEntity.Id,
            Deadline = warrantEntity.Deadline,
            IsUrgent = warrantEntity.IsUrgent,
            Title = warrantEntity.Title,
            Number = warrantEntity.Number,
            TechnicianId = warrantEntity.TechnicianId,
            Procedure = new ProcedureSummaryModel()
            {
                Id = procedure.Id,
                Color = procedure.Color,
                Name = procedure.Name,
                Priority = procedure.Priority
            },
            CanBeAdvancedByFrontOffice = nextTransition?.CanBePerformedByFrontOffice == true,
            CanBeAdvancedByWorkshop = nextTransition?.CanBePerformedByWorkshop == true,
            CanBeRolledBackByFrontOffice = previousTransition?.CanBePerformedByFrontOffice == true,
            CanBeRolledBackByWorkshop = previousTransition?.CanBePerformedByWorkshop == true,
            NextStepId = currentStep?.NextStep?.Id,
            PreviousStepId = currentStep?.PreviousStep?.Id,
        };

        return warrantModel;
    }
}
