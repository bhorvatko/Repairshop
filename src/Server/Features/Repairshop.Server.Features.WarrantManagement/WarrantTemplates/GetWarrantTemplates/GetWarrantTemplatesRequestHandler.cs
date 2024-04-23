using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Server.Features.WarrantManagement.WarrantTemplates.GetWarrantTemplates;

internal class GetWarrantTemplatesRequestHandler
    : IRequestHandler<GetWarrantTemplatesRequest, GetWarrantTemplatesResponse>
{
    private readonly IRepository<WarrantTemplate> _warrantTemplates;

    public GetWarrantTemplatesRequestHandler(IRepository<WarrantTemplate> warrantTemplates)
    {
        _warrantTemplates = warrantTemplates;
    }

    public async Task<GetWarrantTemplatesResponse> Handle(
        GetWarrantTemplatesRequest request,
        CancellationToken cancellationToken)
    {
        GetWarrantTempplateModelSpecification specification = new();

        IReadOnlyCollection<WarrantTemplateModel> warrantTemplateModels =
            await _warrantTemplates.ListAsync(specification, cancellationToken);


        return new GetWarrantTemplatesResponse()
        {
            WarrantTemplates = warrantTemplateModels
        };
    }
}
