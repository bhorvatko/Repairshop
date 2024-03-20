using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public class WarrantStep
{
    private WarrantStep(
        Procedure procedure, 
        bool canBeTransitionedToByFrontDesk, 
        bool canBeTransitionedToByWorkshop)
    {
        Procedure = procedure;
        CanBeTransitionedToByFrontDesk = canBeTransitionedToByFrontDesk;
        CanBeTransitionedToByWorkshop = canBeTransitionedToByWorkshop;
    }

    public Procedure Procedure { get; private set; }
    public bool CanBeTransitionedToByFrontDesk { get; set; }
    public bool CanBeTransitionedToByWorkshop { get; set; }

    public static WarrantStep CreateNew(Procedure procedure)
    {
        return new WarrantStep(procedure, false, false);
    }
}
