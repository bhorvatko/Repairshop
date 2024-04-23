using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Client.Infrastructure.Services;

internal class WarrantTemplateService
    : IWarrantTemplateService
{
    private const string WarrantTemplatesEndpoint = "WarrantTemplates";

    private readonly ApiClient.ApiClient _apiClient;

    public WarrantTemplateService(ApiClient.ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task CreateWarrantTemplate(
        string name,
        IEnumerable<CreateWarrantStepDto> steps)
    {
        CreateWarrantTemplateRequest request = new()
        {
            Name = name,
            Steps = steps.Select(x => x.ToWarrantStepDto()).ToList()
        };

        await _apiClient.Post<CreateWarrantTemplateRequest, CreateWarrantTemplateResponse>(
            WarrantTemplatesEndpoint,
            request);
    }
}
