using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Infrastructure.Services;

internal class ProcedureService
    : IProcedureService
{
    private const string ProceduresEndpoint = "Procedures";

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

    public async Task<IEnumerable<Procedure>> GetProcedures()
    {
        GetProceduresResponse response = 
            await _apiClient.Get<GetProceduresResponse>(ProceduresEndpoint);

        return response
            .Procedures
            .Select(x => Procedure.Create(x.Id, x.Name, x.Color));
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
}
