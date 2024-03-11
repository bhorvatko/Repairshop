namespace Repairshop.Shared.Features.WarrantManagement.Procedures;

public class ProcedureModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Color { get; set; }
}
