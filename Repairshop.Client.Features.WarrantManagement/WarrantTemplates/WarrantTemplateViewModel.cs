using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Client.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

public class WarrantTemplateViewModel
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required IReadOnlyCollection<WarrantStep> Steps { get; set; }

    public IReadOnlyCollection<Procedure> Procedures => 
        Steps.Select(x => x.Procedure).ToList();
}
