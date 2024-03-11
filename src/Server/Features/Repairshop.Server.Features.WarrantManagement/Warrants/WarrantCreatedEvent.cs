using Repairshop.Server.Common.Events;

namespace Repairshop.Server.Features.WarrantManagement.Warrants;

internal class WarrantCreatedEvent
    : DomainEvent
{
    private WarrantCreatedEvent(Warrant warrant)
    {
        Warrant = warrant;
    }

    public Warrant Warrant { get; set; }

    public static WarrantCreatedEvent Create(Warrant warrant) =>
        new WarrantCreatedEvent(warrant);
}
