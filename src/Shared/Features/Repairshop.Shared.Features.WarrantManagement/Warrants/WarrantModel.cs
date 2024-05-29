using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class WarrantModel
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required DateTime Deadline { get; set; }
    public required bool IsUrgent { get; set; }
    public required int Number { get; set; }
    public required Guid? TechnicianId { get; set; }
    public required ProcedureSummaryModel Procedure { get; set; }
    public required bool CanBeAdvancedByWorkshop { get; set; }
    public required bool CanBeAdvancedByFrontOffice { get; set; }
    public required bool CanBeRolledBackByWorkshop { get; set; }
    public required bool CanBeRolledBackByFrontOffice { get; set; }
    public required Guid? NextStepId { get; set; }
    public required Guid? PreviousStepId { get; set; }
}
