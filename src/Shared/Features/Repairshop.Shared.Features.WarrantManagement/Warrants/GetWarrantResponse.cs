namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class GetWarrantResponse
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required DateTime Deadline { get; set; }
    public required bool IsUrgent { get; set; }
    public required int Number { get; set; }
    public required Guid ProcedureId { get; set; }
    public required IEnumerable<WarrantStepModel> WarrantSteps { get; set; }
}
