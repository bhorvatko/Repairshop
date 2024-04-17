using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class WarrantModel
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required DateTime Deadline { get; set; }
    public required bool IsUrgent { get; set; }
    public required Guid? TechnicianId { get; set; }
    public required ProcedureModel Procedure { get; set; }
    public required bool CanBeAdvanced { get; set; }
    public required bool CanBeRolledBack { get; set; }
    public required Guid? NextStepId { get; set; }
    public required Guid? PreviousStepId { get; set; }
}
