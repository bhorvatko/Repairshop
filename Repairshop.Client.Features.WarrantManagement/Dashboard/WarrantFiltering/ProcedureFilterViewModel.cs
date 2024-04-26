using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard.WarrantFiltering;

public class ProcedureFilterViewModel
{
    public Procedure Procedure { get; set; }
    public bool Selected { get; set; }

    public ProcedureFilterViewModel(
        Procedure procedure,
        bool selected)
    {
        Procedure = procedure;
        Selected = selected;
    }
}
