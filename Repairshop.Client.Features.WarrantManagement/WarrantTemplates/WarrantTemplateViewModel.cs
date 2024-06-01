using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

public class WarrantTemplateViewModel
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required IReadOnlyCollection<WarrantTemplateStep> Steps { get; set; }

    public IReadOnlyCollection<ProcedureSummaryViewModel> Procedures => 
        Steps.OrderBy(x => x.Index).Select(x => x.Procedure).ToList();
}
