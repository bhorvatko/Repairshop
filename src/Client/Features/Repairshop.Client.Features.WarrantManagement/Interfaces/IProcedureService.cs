namespace Repairshop.Client.Features.WarrantManagement.Procedures;

public interface IProcedureService
{
    Task<Guid> CreateProcedure(string name, string color, float priority);
    Task<IEnumerable<ProcedureSummaryViewModel>> GetProcedureSummaries();
    Task<IReadOnlyCollection<ProcedureViewModel>> GetProcedures();
    Task UpdateProcedure(Guid id, string name, string color);
    Task DeleteProcedure(Guid id);
    Task SetProcedurePriority(Guid id, float prioriy);
}
