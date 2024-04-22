using Ardalis.Specification;

namespace Repairshop.Server.Features.WarrantManagement.Warrants;

internal class GetWarrantSpecification
    : Specification<Warrant>
{
    public GetWarrantSpecification(Guid warrantId)
    {
        Query
            .Include(x => x.CurrentStep).ThenInclude(x => x.Procedure)
            .Include(x => x.Steps).ThenInclude(x => x.NextTransition)
            .Include(x => x.Steps).ThenInclude(x => x.PreviousTransition)
            .Include(x => x.Steps).ThenInclude(x => x.Procedure)
            .Where(x => x.Id == warrantId);
    }
}
