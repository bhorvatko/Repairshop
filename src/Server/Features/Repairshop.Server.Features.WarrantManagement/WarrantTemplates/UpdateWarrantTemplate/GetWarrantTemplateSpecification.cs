using Ardalis.Specification;

namespace Repairshop.Server.Features.WarrantManagement.WarrantTemplates.UpdateWarrantTemplate;

internal class GetWarrantTemplateSpecification
    : Specification<WarrantTemplate>
{
    public GetWarrantTemplateSpecification(Guid warrantTemplateId)
    {
        Query
            .Where(x => x.Id == warrantTemplateId)
            .Include(x => x.Steps);
    }
}
