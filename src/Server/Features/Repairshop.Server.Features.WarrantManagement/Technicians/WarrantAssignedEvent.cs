using Repairshop.Server.Common.Events;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;
internal class WarrantAssignedEvent
    : DomainEvent
{
    private WarrantAssignedEvent(
        Guid warrantId,
        Guid technicianId)
    {
        WarrantId = warrantId;
        TechnicianId = technicianId;
    }

    public Guid WarrantId { get; private set; }
    public Guid TechnicianId { get; private set; }

    public static WarrantAssignedEvent Create(
        Guid warrantId,
        Guid technicianId) =>
        new WarrantAssignedEvent(warrantId, technicianId);
}
