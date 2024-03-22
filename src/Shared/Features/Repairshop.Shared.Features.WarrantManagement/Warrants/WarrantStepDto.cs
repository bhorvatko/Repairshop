namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class WarrantStepDto
{
    public required Guid ProcedureId { get; set; }
    public required bool CanBeTransitionedToByFrontDesk { get; set; }
    public required bool CanBeTransitionedToByWorkshop { get; set; }
}
