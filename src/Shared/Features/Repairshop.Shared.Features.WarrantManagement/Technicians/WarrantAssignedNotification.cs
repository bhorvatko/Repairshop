using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Shared.Features.WarrantManagement.Technicians;

public class WarrantAssignedNotification
{
    public required WarrantModel Warrant { get; set; }
    public required Guid? ToTechnicianId { get; set; }
    public required Guid? FromTechnicianId { get; set; }
}
