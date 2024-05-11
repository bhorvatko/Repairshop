using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Shared.Features.WarrantManagement.Technicians;

namespace Repairshop.Client.Infrastructure.Services;

internal class TechnicianService
    : ITechnicianService
{
    private const string TechniciansEndpoint = "Technicians";
    private const string AssignWarrantEndpoint = $"{TechniciansEndpoint}/AssignWarrant";

    private readonly ApiClient.ApiClient _apiClient;
    private readonly TechnicianViewModelFactory _technicianViewModelFactory;

    public TechnicianService(
        ApiClient.ApiClient apiClient,
        TechnicianViewModelFactory technicianViewModelFactory)
    {
        _apiClient = apiClient;
        _technicianViewModelFactory = technicianViewModelFactory;
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
            .Select(_technicianViewModelFactory.Create);
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

    public async Task UpdateTechnician(Guid technicianId, string name)
    {
        UpdateTechnicianRequest request = new()
        {
            TechnicianId = technicianId,
            Name = name
        };

        await _apiClient.Put<UpdateTechnicianRequest, UpdateTechnicianResponse>(
            TechniciansEndpoint,
            request);
    }

    public async Task DeleteTechnician(Guid technicianId)
    {
        await _apiClient.Delete($"{TechniciansEndpoint}/{technicianId}");
    }
}
