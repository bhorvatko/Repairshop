using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

public class WarrantTemplateStep
    : ObservableObject
{
    private bool _canBeTransitionedToByFrontDesk;
    private bool _canBeTransitionedToByWorkshop;

    private WarrantTemplateStep(
        ProcedureSummaryViewModel procedure,
        bool canBeTransitionedToByFrontDesk,
        bool canBeTransitionedToByWorkshop,
        int index)
    {
        Procedure = procedure;
        CanBeTransitionedToByFrontDesk = canBeTransitionedToByFrontDesk;
        CanBeTransitionedToByWorkshop = canBeTransitionedToByWorkshop;
        Index = index;
    }

    public ProcedureSummaryViewModel Procedure { get; private set; }
    public int Index { get; private set; }

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

    public static WarrantTemplateStep Create(
        ProcedureSummaryViewModel procedure,
        bool canBeTransitionedToByFrontDesk,
        bool canBeTransitionedToByWorkshop,
        int index)
    {
        return new WarrantTemplateStep(
            procedure,
            canBeTransitionedToByFrontDesk,
            canBeTransitionedToByWorkshop,
            index);
    }
}
