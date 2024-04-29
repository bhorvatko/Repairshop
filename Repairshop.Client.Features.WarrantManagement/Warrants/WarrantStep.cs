using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public class WarrantStep
    : ObservableObject
{
    private bool _canBeTransitionedToByFrontDesk;
    private bool _canBeTransitionedToByWorkshop;

    private WarrantStep(
        ProcedureSummaryViewModel procedure, 
        bool canBeTransitionedToByFrontDesk, 
        bool canBeTransitionedToByWorkshop)
    {
        Procedure = procedure;
        CanBeTransitionedToByFrontDesk = canBeTransitionedToByFrontDesk;
        CanBeTransitionedToByWorkshop = canBeTransitionedToByWorkshop;
    }

    public ProcedureSummaryViewModel Procedure { get; private set; }

    public bool CanBeTransitionedToByFrontDesk
    { 
        get => _canBeTransitionedToByFrontDesk; 
        set
        {
            SetProperty(ref _canBeTransitionedToByFrontDesk, value);

            if (value == false)
            {
                CanBeTransitionedToByWorkshop = true;
            }
        }
    }

    public bool CanBeTransitionedToByWorkshop
    { 
        get => _canBeTransitionedToByWorkshop; 
        set
        {
            SetProperty(ref _canBeTransitionedToByWorkshop, value);

            if (value == false)
            {
                CanBeTransitionedToByFrontDesk = true;
            }
        }
    }

    public static WarrantStep CreateNew(ProcedureSummaryViewModel procedure)
    {
        return new WarrantStep(procedure, true, true);
    }

    public static WarrantStep Create(
        ProcedureSummaryViewModel procedure,
        bool canBeTransitionedToByFrontDesk,
        bool canBeTransitionedToByWorkshop)
    {
        return new WarrantStep(
            procedure, 
            canBeTransitionedToByFrontDesk, 
            canBeTransitionedToByWorkshop);
    }
}
