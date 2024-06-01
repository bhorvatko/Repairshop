using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Features.WarrantManagement.WarrantTemplates;
using Repairshop.Client.Infrastructure.Services.Extensions;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
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

    public async Task UpdateWarrantTemplate(
        Guid warrantTemplateId,
        string name,
        IEnumerable<CreateWarrantStepDto> steps)
    {
        UpdateWarrantTemplateRequest request = new()
        {
            Id = warrantTemplateId,
            Name = name,
            Steps = steps.Select(x => x.ToWarrantStepDto()).ToList()
        };

        await _apiClient.Put<UpdateWarrantTemplateRequest, UpdateWarrantTemplateResponse>(
            WarrantTemplatesEndpoint,
            request);
    }

    public async Task<IReadOnlyCollection<WarrantTemplateViewModel>> GetWarrantTemplates()
    {
        GetWarrantTemplatesResponse response = 
            await _apiClient.Get<GetWarrantTemplatesResponse>(WarrantTemplatesEndpoint);

        return 
            response
                .WarrantTemplates
                .Select(x => new WarrantTemplateViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Steps = x.Steps
                        .Select(s => WarrantTemplateStep.Create(
                            s.Procedure.ToViewModel(),
                            s.CanBeTransitionedToByFrontOffice,
                            s.CanBeTransitionedToByWorkshop,
                            s.Index))
                        .ToList()
                })
                .ToList();
    }
}
