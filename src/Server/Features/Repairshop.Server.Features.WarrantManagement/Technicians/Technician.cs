using Repairshop.Server.Common.Entities;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;

public class Technician
    : EntityBase
{
#nullable disable
    private Technician() { }
#nullable enable

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyCollection<Warrant> Warrants { get; private set; }

    public static Technician Create(string name)
    {
        return new Technician()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Warrants = Array.Empty<Warrant>()
        };
    }

    public void AssignWarrant(Warrant warrant)
    {
        Guid? previousTechnicianId = warrant.TechnicianId;

        if (previousTechnicianId == Id)
        {
            throw new DomainArgumentException(
                warrant,
                "Warrant is already assigned to the technician");
        }

        Warrants = Warrants?.Append(warrant).ToList() ?? new[] { warrant }.ToList();

        warrant.AssignTo(this);

        AddEvent(WarrantAssignedEvent.Create(warrant, Id, previousTechnicianId));
    }
}
