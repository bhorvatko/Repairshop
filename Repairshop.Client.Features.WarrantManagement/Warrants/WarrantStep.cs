using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public class WarrantStep
{
    public Procedure Procedure { get; private set; }
    public bool CanBeTransitionedToByFrontDesk { get; private set; }
    public bool CanBeTransitionedToByWorkshop { get; private set; }
}
