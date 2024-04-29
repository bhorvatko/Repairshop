using Ardalis.Specification;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.GetWarrant;

internal class GetWarrantResponseSpecification
    : Specification<Warrant, GetWarrantResponse>
{
    public GetWarrantResponseSpecification(Guid id)
    {
        Query.Where(x => x.Id == id);

        Query.Select(x => new GetWarrantResponse()
        {
            Id = x.Id,
            Deadline = x.Deadline,
            IsUrgent = x.IsUrgent,
            Title = x.Title,
            WarrantSteps = x.Steps.Select(s => new WarrantStepModel()
            {
                CanBeTransitionedToByFrontOffice = 
                    s.PreviousTransition != null
                        ? s.PreviousTransition.CanBePerformedByFrontOffice
                        : false,
                CanBeTransitionedToByWorkshop =
                    s.PreviousTransition != null
                        ? s.PreviousTransition.CanBePerformedByWorkshop
                        : false,
                Procedure = new ProcedureSummaryModel() 
                {
                    Id = s.ProcedureId,
                    Color = s.Procedure.Color,
                    Name = s.Procedure.Name,
                }
            })
        });
    }
}
