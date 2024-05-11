using Ardalis.Specification;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;

internal class GetTechnicianSpecification
    : Specification<Technician>
{
    public GetTechnicianSpecification(params Guid[] ids)
    {
        Query
            .Where(t => ids.Contains(t.Id))
            .Include(t => t.Warrants);
    }
}
