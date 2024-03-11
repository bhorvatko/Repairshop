namespace Repairshop.Shared.Features.WarrantManagement.Procedures;

public class GetProceduresResponse
{
    public required IEnumerable<ProcedureModel> Procedures { get; set; }
}
