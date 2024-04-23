namespace Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;

public class GetWarrantTemplatesResponse
{
    public required IEnumerable<WarrantTemplateModel> WarrantTemplates { get; set; }
}