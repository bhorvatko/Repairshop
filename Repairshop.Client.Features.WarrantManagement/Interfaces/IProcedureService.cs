namespace Repairshop.Client.Features.WarrantManagement.Procedures;

public interface IProcedureService
{
    Task CreateProcedure(string name, string color);
    Task<IEnumerable<Procedure>> GetProcedures();
    Task UpdateProcedure(Guid id, string name, string color);
}
