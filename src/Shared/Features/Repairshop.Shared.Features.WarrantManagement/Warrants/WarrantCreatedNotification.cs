namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class WarrantCreatedNotification
{
    public required Guid? TechnicianId { get; set; }
    public required WarrantModel WarrantModel { get; set; }
}
