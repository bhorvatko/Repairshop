using Ardalis.Specification;
using Repairshop.Server.Features.WarrantManagement.Warrants.GetWarrants;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;
internal class GetTechnicianModelsSpecifcation
    : Specification<Technician, TechnicianQueryModel>
{
    public GetTechnicianModelsSpecifcation()
    {
        Query.AsNoTracking();

        Query.Select(x => new TechnicianQueryModel()
        {
            Id = x.Id,
            Name = x.Name,
            Warrants = x.Warrants.Select(w => new WarrantQueryModel()
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
                CanBeRolledBakByWorkshop =
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
                Procedure = new ProcedureModel()
                {
                    Id = w.CurrentStep!.Procedure.Id,
                    Color = w.CurrentStep!.Procedure.Color,
                    Name = w.CurrentStep!.Procedure.Name
                }
            })
        });
    }
}
