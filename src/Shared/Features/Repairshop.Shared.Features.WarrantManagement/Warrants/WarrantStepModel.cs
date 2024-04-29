using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class WarrantStepModel
{
    public required ProcedureSummaryModel Procedure { get; set; }
    public bool CanBeTransitionedToByFrontOffice { get; set; }
    public bool CanBeTransitionedToByWorkshop { get; set; }
}
