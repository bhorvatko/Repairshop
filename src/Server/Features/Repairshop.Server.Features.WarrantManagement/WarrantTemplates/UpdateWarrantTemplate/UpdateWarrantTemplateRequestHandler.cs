using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Server.Features.WarrantManagement.WarrantTemplates.UpdateWarrantTemplate;

internal class UpdateWarrantTemplateRequestHandler
    : IRequestHandler<UpdateWarrantTemplateRequest, UpdateWarrantTemplateResponse>
{
    private readonly WarrantTemplateStepSequenceFactory _warrantTemplateStepSequenceFactory;
    private readonly IRepository<WarrantTemplate> _warrantTemplates;

    public UpdateWarrantTemplateRequestHandler(
        WarrantTemplateStepSequenceFactory warrantTemplateStepSequenceFactory,
        IRepository<WarrantTemplate> warrantTemplates)
    {
        _warrantTemplateStepSequenceFactory = warrantTemplateStepSequenceFactory;
        _warrantTemplates = warrantTemplates;
    }

    public async Task<UpdateWarrantTemplateResponse> Handle(
        UpdateWarrantTemplateRequest request,
        CancellationToken cancellationToken)
    {
        GetWarrantTemplateSpecification specification = new(request.Id);

        WarrantTemplate? warrantTemplate =
            await _warrantTemplates.FirstOrDefaultAsync(specification, cancellationToken);

        if (warrantTemplate is null)
        {
            throw new EntityNotFoundException<WarrantTemplate, Guid>(request.Id);
        }

        IReadOnlyCollection<WarrantTemplateStep> stepSequence =
            await _warrantTemplateStepSequenceFactory.CreateWarrantTemplateStepSequence(
                request.Steps,
                cancellationToken);

        warrantTemplate.Update(
            request.Name, 
            stepSequence);

        await _warrantTemplates.SaveChangesAsync(cancellationToken);

        return new UpdateWarrantTemplateResponse();
    }
}
