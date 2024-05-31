using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Server.Features.WarrantManagement.WarrantTemplates.CreateWarrantTemplate;

internal class CreateWarrantTemplateRequestHandler
    : IRequestHandler<CreateWarrantTemplateRequest, CreateWarrantTemplateResponse>
{
    private readonly WarrantTemplateStepSequenceFactory _warrantTemplateStepSequenceFactory;
    private readonly IRepository<WarrantTemplate> _warrantTemplates;

    public CreateWarrantTemplateRequestHandler(
        WarrantTemplateStepSequenceFactory warrantTemplateStepSequenceFactory, 
        IRepository<WarrantTemplate> warrantTemplates)
    {
        _warrantTemplateStepSequenceFactory = warrantTemplateStepSequenceFactory;
        _warrantTemplates = warrantTemplates;
    }

    public async Task<CreateWarrantTemplateResponse> Handle(
        CreateWarrantTemplateRequest request, 
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<WarrantTemplateStep> stepSequence =
            await _warrantTemplateStepSequenceFactory.CreateWarrantTemplateStepSequence(
                request.Steps, 
                cancellationToken);

        WarrantTemplate warrantTemplate = 
            WarrantTemplate.Create(request.Name, stepSequence);

        await _warrantTemplates.AddAsync(warrantTemplate, cancellationToken);

        return new CreateWarrantTemplateResponse();
    }
}
