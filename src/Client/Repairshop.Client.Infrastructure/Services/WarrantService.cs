using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Warrants;
using Repairshop.Client.Infrastructure.Services.Extensions;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Infrastructure.Services;

internal class WarrantService
    : IWarrantService
{
    private const string WarrantsEndpoint = "Warrants";
    private const string AdvanceWarrantEndpoint = $"{WarrantsEndpoint}/Advance";
    private const string RollbackWarrantEndpoint = $"{WarrantsEndpoint}/Rollback";

    private readonly ApiClient.ApiClient _apiClient;
    private readonly WarrantSummaryViewModelFactory _warrantSummaryViewModelFactory;

    public WarrantService(
        ApiClient.ApiClient apiClient, 
        WarrantSummaryViewModelFactory warrantSummaryViewModelFactory)
    {
        _apiClient = apiClient;
        _warrantSummaryViewModelFactory = warrantSummaryViewModelFactory;
    }

    public async Task CreateWarrant(
        string title, 
        DateTime deadline, 
        bool isUrgent, 
        int number,
        IEnumerable<CreateWarrantStepDto> steps)
    {
        CreateWarrantRequest request = new CreateWarrantRequest()
        {
            Title = title,
            Deadline = deadline,
            IsUrgnet = isUrgent,
            Number = number,
            Steps = steps.Select(x => x.ToWarrantStepDto())
        };

        await _apiClient.Post<CreateWarrantRequest, CreateWarrantResponse>(
            WarrantsEndpoint,
            request);
    }

    public async Task<IEnumerable<WarrantSummaryViewModel>> GetUnassignedWarrants()
    {
        GetWarrantsRequest request = new()
        {
            TechnicianId = null
        };

        GetWarrantsResponse response = 
            await _apiClient.Get<GetWarrantsResponse>(WarrantsEndpoint, request);

        return response.Warrants.Select(_warrantSummaryViewModelFactory.Create);
    }

    public async Task UpdateWarrant(
        Guid id, 
        string title, 
        DateTime deadline, 
        bool isUrgent, 
        int number, 
        IEnumerable<CreateWarrantStepDto> steps,
        Guid? currentStepProcedureId)
    {
        UpdateWarrantRequest request = new()
        {
            Id = id,
            Title = title,
            Deadline = deadline,
            IsUrgent = isUrgent,
            Number = number,
            Steps = steps.Select(x => x.ToWarrantStepDto()),
            CurrentStepProcedureId = currentStepProcedureId
        };

        await _apiClient
            .Put<UpdateWarrantRequest, UpdateWarrantResponse>($"{WarrantsEndpoint}", request);
    }

    public async Task<WarrantViewModel> GetWarrant(Guid id)
    {
        GetWarrantResponse response =
            await _apiClient.Get<GetWarrantResponse>($"{WarrantsEndpoint}/{id}");

        return WarrantViewModel.Create(
            response.Id,
            response.IsUrgent,
            response.Deadline,
            response.Title,
            response.Number,
            response.ProcedureId,
            response.WarrantSteps.Select(x => x.ToViewModel()));
    }

    public async Task AdvanceWarrant(Guid warrantId, Guid stepId)
    {
        AdvanceWarrantRequest request = new()
        {
            StepId = stepId,
            WarrantId = warrantId
        };

        await _apiClient.Post<AdvanceWarrantRequest, AdvanceWarrantResponse>(
            AdvanceWarrantEndpoint, 
            request);
    }

    public async Task RollbackWarrant(Guid warrantId, Guid stepId)
    {
        RollbackWarrantRequest request = new()
        {
            StepId = stepId,
            WarrantId = warrantId
        };

        await _apiClient.Post<RollbackWarrantRequest, RollbackWarrantResponse>(
            RollbackWarrantEndpoint,
            request);
    }

    public async Task UnassignWarrant(Guid warrantId)
    {
        await _apiClient.Put<UnassignWarrantResponse>($"Warrants/{warrantId}/Unassign");
    }
}
