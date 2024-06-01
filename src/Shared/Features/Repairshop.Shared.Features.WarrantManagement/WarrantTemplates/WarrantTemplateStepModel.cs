using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;

public class WarrantTemplateStepModel
{
    public required ProcedureSummaryModel Procedure { get; set; }
    public bool CanBeTransitionedToByFrontOffice { get; set; }
    public bool CanBeTransitionedToByWorkshop { get; set; }
    public required int Index { get; set; }
}