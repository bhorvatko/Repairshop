using Repairshop.Server.Common.Events;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.ProcedureChanged;

internal class WarrantProcedureChangedEvent
    : DomainEvent
{
    private WarrantProcedureChangedEvent(Warrant warrant)
    {
        Warrant = warrant;
    }

    public Warrant Warrant { get; set; }

    public static WarrantProcedureChangedEvent Create(Warrant warrant) =>
        new WarrantProcedureChangedEvent(warrant);
}
