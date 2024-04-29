namespace Repairshop.Client.Features.WarrantManagement.Procedures;

public interface IProcedureService
{
    Task CreateProcedure(string name, string color);
    Task<IEnumerable<ProcedureSummaryViewModel>> GetProcedureSummaries();
    Task<IReadOnlyCollection<ProcedureViewModel>> GetProcedures();
    Task UpdateProcedure(Guid id, string name, string color);
    Task DeleteProcedure(Guid id);
}
