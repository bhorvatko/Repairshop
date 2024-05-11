namespace Repairshop.Client.Features.WarrantManagement.Dashboard;
public interface ITechnicianService
{
    Task<IEnumerable<TechnicianViewModel>> GetTechnicians();
    Task CreateTechnician(string name);
    Task AssignWarrant(Guid technicianId, Guid warrantId);
    Task UpdateTechnician(Guid technicianId, string name);
}
