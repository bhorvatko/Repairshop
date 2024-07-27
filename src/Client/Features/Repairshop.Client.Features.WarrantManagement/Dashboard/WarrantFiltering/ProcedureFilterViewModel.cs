using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard.WarrantFiltering;

public class ProcedureFilterViewModel
{
    public ProcedureSummaryViewModel Procedure { get; set; }
    public bool Selected { get; set; }

    public ProcedureFilterViewModel(
        ProcedureSummaryViewModel procedure,
        bool selected)
    {
        Procedure = procedure;
        Selected = selected;
    }
}
