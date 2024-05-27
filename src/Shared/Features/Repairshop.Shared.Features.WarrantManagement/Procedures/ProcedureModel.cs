namespace Repairshop.Shared.Features.WarrantManagement.Procedures;

public class ProcedureModel
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Color { get; set; }
    public required float Priority { get; set; }
    public required IEnumerable<string> UsedByWarrants { get; set; }
    public required IEnumerable<string> UsedByWarrantTemplates { get; set; }
}
