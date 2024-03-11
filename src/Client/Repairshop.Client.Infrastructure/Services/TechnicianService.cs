using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Technicians;

namespace Repairshop.Client.Infrastructure.Services;

internal class TechnicianService
    : ITechnicianService
{
    private const string TechniciansEndpoint = "Technicians";

    private readonly ApiClient.ApiClient _apiClient;

    public TechnicianService(ApiClient.ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task CreateTechnician(string name)
    {
        CreateTechnicianRequest request = new() { Name = name};

        await _apiClient.Post<CreateTechnicianRequest, CreateTechnicianResponse>(
            TechniciansEndpoint, 
            request);
    }

    public async Task<IEnumerable<TechnicianViewModel>> GetTechnicians()
    {
        GetTechniciansResponse response =
            await _apiClient.Get<GetTechniciansResponse>(TechniciansEndpoint);

        return response
            .Technicians
            .Select(x => TechnicianViewModel.Create(
                x.Name,
                x.Warrants.Select(w => 
                    WarrantViewModel.Create(
                        w.IsUrgent,
                        w.Deadline,
                        Procedure.Create(w.Procedure.Id, w.Procedure.Name, w.Procedure.Color),
                        w.Title))));
    }
}
