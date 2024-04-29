using Ardalis.Specification;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.GetWarrants;

internal class GetWarrantModelsSpecification
    : Specification<Warrant, WarrantModel>
{
    public GetWarrantModelsSpecification(Guid? technicianId)
    {
        Query.Where(x => x.TechnicianId == technicianId);

        Query.Select(x => new WarrantModel()
        {
            Id = x.Id,
            Deadline = x.Deadline,
            IsUrgent = x.IsUrgent,
            TechnicianId = x.TechnicianId,
            Title = x.Title,
            CanBeAdvancedByFrontOffice =
                x.CurrentStep!.NextTransition != null
                    ? x.CurrentStep!.NextTransition.CanBePerformedByFrontOffice
                    : false,
            CanBeRolledBackByFrontOffice =
                x.CurrentStep!.PreviousTransition != null
                     ? x.CurrentStep!.PreviousTransition!.CanBePerformedByFrontOffice
                     : false,
            CanBeAdvancedByWorkshop =
                x.CurrentStep!.NextTransition != null
                    ? x.CurrentStep!.NextTransition!.CanBePerformedByWorkshop
                    : false,
            CanBeRolledBackByWorkshop =
                x.CurrentStep!.PreviousTransition != null
                    ? x.CurrentStep!.PreviousTransition!.CanBePerformedByWorkshop
                    : false,
            NextStepId =
                x.CurrentStep.NextTransition != null
                    ? x.CurrentStep.NextTransition.NextStep.Id
                    : null,
            PreviousStepId =
                x.CurrentStep.PreviousTransition != null
                    ? x.CurrentStep.PreviousTransition.PreviousStep.Id
                    : null,
            Procedure = new ProcedureSummaryModel()
            {
                Id = x.CurrentStep!.Procedure.Id,
                Color = x.CurrentStep!.Procedure.Color,
                Name = x.CurrentStep!.Procedure.Name
            }
        });
    }
}
