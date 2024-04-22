using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;
using static Repairshop.Server.Features.WarrantManagement.WarrantTemplates.WarrantTemplateStep;

namespace Repairshop.Server.Features.WarrantManagement.WarrantTemplates.CreateWarrantTemplate;

internal class CreateWarrantTemplateRequestHandler
    : IRequestHandler<CreateWarrantTemplateRequest, CreateWarrantTemplateResponse>
{
    private readonly IRepository<Procedure> _procedures;
    private readonly IRepository<WarrantTemplate> _warrantTemplates;

    public CreateWarrantTemplateRequestHandler(
        IRepository<Procedure> procedures, 
        IRepository<WarrantTemplate> warrantTemplates)
    {
        _procedures = procedures;
        _warrantTemplates = warrantTemplates;
    }

    public async Task<CreateWarrantTemplateResponse> Handle(
        CreateWarrantTemplateRequest request, 
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<CreateWarrantStepArgs> warrantStepArgs =
            request.Steps
                .Select(x => new CreateWarrantStepArgs(
                    x.ProcedureId,
                    x.CanBeTransitionedToByFrontDesk,
                    x.CanBeTransitionedToByWorkshop))
                .ToList();

        GetProceduresByIdDelegate getProceduresById =
            async ids => await _procedures.ListAsync(new GetProceduresSpecification(ids), cancellationToken);

        IReadOnlyCollection<WarrantTemplateStep> stepSequence =
            await WarrantTemplateStep.CreateStepSequence(warrantStepArgs, getProceduresById);

        WarrantTemplate warrantTemplate = 
            WarrantTemplate.Create(request.Name, stepSequence);

        await _warrantTemplates.AddAsync(warrantTemplate, cancellationToken);

        return new CreateWarrantTemplateResponse();
    }
}
