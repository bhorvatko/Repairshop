using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Client.Infrastructure.Services.Extensions;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Infrastructure.Services;

internal class ProcedureService
    : IProcedureService
{
    private const string ProceduresEndpoint = "Procedures";
    private const string ProcedureSummariesEndpoint = $"{ProceduresEndpoint}/Summaries";

    private readonly ApiClient.ApiClient _apiClient;

    public ProcedureService(ApiClient.ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task CreateProcedure(string name, string color)
    {
        await _apiClient.Post<CreateProcedureRequest, CreateProcedureResponse>(
            ProceduresEndpoint,
            new CreateProcedureRequest()
            {
                Color = color,
                Name = name
            });
    }

    public async Task<IEnumerable<ProcedureSummaryViewModel>> GetProcedureSummaries()
    {
        GetProcedureSummariesResponse response = 
            await _apiClient.Get<GetProcedureSummariesResponse>(ProcedureSummariesEndpoint);

        return response
            .Procedures
            .Select(x => x.ToViewModel());
    }

    public async Task<IReadOnlyCollection<ProcedureViewModel>> GetProcedures()
    {
        GetProceduresResponse response =
            await _apiClient.Get<GetProceduresResponse>(ProceduresEndpoint);

        return response
            .Procedures
            .Select(x => x.ToViewModel())
            .ToList();
    }

    public async Task UpdateProcedure(Guid id, string name, string color)
    {
        UpdateProcedureRequest request = new UpdateProcedureRequest()
        {
            Id = id,
            Name = name,
            Color = color
        };

        await _apiClient
            .Put<UpdateProcedureRequest, UpdateProcedureResponse>(
                ProceduresEndpoint, 
                request);
    }

    public Task DeleteProcedure(Guid id) => 
        _apiClient.Delete($"{ProceduresEndpoint}/{id}");
}
