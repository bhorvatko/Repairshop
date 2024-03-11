namespace Repairshop.Shared.Features.WarrantManagement.Technicians;

public class WarrantAssignedNotification
{
    public required Guid WarrantId { get; set; }
    public required Guid TechnicianId { get; set; }
}
