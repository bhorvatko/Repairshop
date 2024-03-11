namespace Repairshop.Client.Features.WarrantManagement.Dashboard;
public interface ITechnicianService
{
    Task<IEnumerable<TechnicianViewModel>> GetTechnicians();
    Task CreateTechnician(string name);
}
