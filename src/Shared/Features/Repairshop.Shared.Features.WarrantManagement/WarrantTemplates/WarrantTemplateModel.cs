using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;

public class WarrantTemplateModel
{
    public required string Name { get; set; }
    public required IEnumerable<WarrantStepModel> Steps { get; set; }
}