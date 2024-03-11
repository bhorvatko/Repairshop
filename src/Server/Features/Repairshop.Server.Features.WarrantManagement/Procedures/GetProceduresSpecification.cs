using Ardalis.Specification;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;
internal class GetProceduresSpecification
    : Specification<Procedure>
{
    public GetProceduresSpecification(IEnumerable<Guid>? ids = null)
    {
        if (ids is not null)
        {
            Query
                .Where(x => ids.Contains(x.Id));
        }
    }
}
