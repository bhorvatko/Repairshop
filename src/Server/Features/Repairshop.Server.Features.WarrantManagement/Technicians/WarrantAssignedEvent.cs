using Repairshop.Server.Common.Events;
using Repairshop.Server.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;
internal class WarrantAssignedEvent
    : DomainEvent
{
    private WarrantAssignedEvent(
        Warrant warrant,
        Guid toTechnicianId,
        Guid? fromTechnicianId)
    {
        Warrant = warrant;
        ToTechnicianId = toTechnicianId;
        FromTechnicianId = fromTechnicianId;
    }

    public Warrant Warrant { get; private set; }
    public Guid ToTechnicianId { get; private set; }
    public Guid? FromTechnicianId { get; private set; }

    public static WarrantAssignedEvent Create(
        Warrant warrant,
        Guid toTechnicianId,
        Guid? fromTechnicianId) =>
        new WarrantAssignedEvent(warrant, toTechnicianId, fromTechnicianId);
}
