using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Warrants;

internal class WarrantQueryModel
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required DateTime Deadline { get; set; }
    public required bool IsUrgent { get; set; }
    public required Guid? TechnicianId { get; set; }
    public required ProcedureModel Procedure { get; set; }
    public required bool CanBeAdvancedByFrontOffice { get; set; }
    public required bool CanBeRolledBackByFrontOffice { get; set; }
    public required bool CanBeAdvancedByWorkshop { get; set; }
    public required bool CanBeRolledBakByWorkshop { get; set; }
}
