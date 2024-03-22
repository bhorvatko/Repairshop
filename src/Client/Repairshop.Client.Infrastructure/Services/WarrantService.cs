using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Infrastructure.Services;

internal class WarrantService
    : IWarrantService
{
    private const string WarrantsEndpoint = "Warrants";

    private readonly ApiClient.ApiClient _apiClient;

    public WarrantService(ApiClient.ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task CreateWarrant(
        string title, 
        DateTime deadline, 
        bool isUrgent, 
        IEnumerable<CreateWarrantStepDto> steps)
    {
        CreateWarrantRequest request = new CreateWarrantRequest()
        {
            Title = title,
            Deadline = deadline,
            IsUrgnet = isUrgent,
            Steps = steps.Select(x => new WarrantStepDto()
            {
                CanBeTransitionedToByFrontDesk = x.CanBeTransitionedToByFrontDesk,
                CanBeTransitionedToByWorkshop = x.CanBeTransitionedToByWorkshop,
                ProcedureId = x.ProcedureId,
            })
        };

        await _apiClient.Post<CreateWarrantRequest, CreateWarrantResponse>(
            WarrantsEndpoint,
            request);
    }

    public async Task<IEnumerable<WarrantViewModel>> GetUnassignedWarrants()
    {
        GetWarrantsRequest request = new()
        {
            TechnicianId = null
        };

        GetWarrantsResponse response = 
            await _apiClient.Get<GetWarrantsResponse>(WarrantsEndpoint, request);

        return response.Warrants.Select(w => w.ToViewModel());
    }
}
