using Ardalis.Specification;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Technicians;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;
internal class GetTechnicianModelsSpecifcation
    : Specification<Technician, TechnicianModel>
{
    public GetTechnicianModelsSpecifcation()
    {
        Query.AsNoTracking();

        Query.Select(x => new TechnicianModel()
        {
            Id = x.Id,
            Name = x.Name,
            Warrants = x.Warrants.Select(w => new WarrantModel()
            {
                Id = w.Id,
                Deadline = w.Deadline,
                IsUrgent = w.IsUrgent,
                TechnicianId = w.TechnicianId,
                Title = w.Title,
                CanBeAdvancedByFrontOffice =
                    w.CurrentStep!.NextTransition != null
                        ? w.CurrentStep!.NextTransition.CanBePerformedByFrontOffice
                        : false,
                CanBeRolledBackByFrontOffice =
                    w.CurrentStep!.PreviousTransition != null
                         ? w.CurrentStep!.PreviousTransition!.CanBePerformedByFrontOffice
                         : false,
                CanBeAdvancedByWorkshop =
                    w.CurrentStep!.NextTransition != null
                        ? w.CurrentStep!.NextTransition!.CanBePerformedByWorkshop
                        : false,
                CanBeRolledBackByWorkshop =
                    w.CurrentStep!.PreviousTransition != null
                        ? w.CurrentStep!.PreviousTransition!.CanBePerformedByWorkshop
                        : false,
                NextStepId =
                    w.CurrentStep.NextTransition != null
                        ? w.CurrentStep.NextTransition.NextStep.Id
                        : null,
                PreviousStepId =
                    w.CurrentStep.PreviousTransition != null
                        ? w.CurrentStep.PreviousTransition.PreviousStep.Id
                        : null,
                Procedure = new ProcedureSummaryModel()
                {
                    Id = w.CurrentStep!.Procedure.Id,
                    Color = w.CurrentStep!.Procedure.Color,
                    Name = w.CurrentStep!.Procedure.Name
                }
            })
        });
    }
}
