using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Technicians;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Infrastructure.Services;

internal class TechnicianService
    : ITechnicianService
{
    private const string TechniciansEndpoint = "Technicians";
    private const string AssignWarrantEndpoint = $"{TechniciansEndpoint}/AssignWarrant";

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
            .Select(x => x.ToViewModel());
    }

    public async Task AssignWarrant(
        Guid technicianId,
        Guid warrantId)
    {
        AssignWarrantRequest request = new()
        {
            TechnicianId = technicianId,
            WarrantId = warrantId
        };

        await _apiClient.Put<AssignWarrantRequest, AssignWArrantResponse>(
            AssignWarrantEndpoint,
            request);
    }
}
