namespace Repairshop.Shared.Features.WarrantManagement.Procedures;

public class GetProcedureSummariesResponse
{
    public required IEnumerable<ProcedureSummaryModel> Procedures { get; set; }
}