using Repairshop.Server.Common.Persistence;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using static Repairshop.Server.Features.WarrantManagement.WarrantTemplates.WarrantTemplateStep;

namespace Repairshop.Server.Features.WarrantManagement.WarrantTemplates;

internal class WarrantTemplateStepSequenceFactory
{
    private readonly IRepository<Procedure> _procedures;

    public WarrantTemplateStepSequenceFactory(IRepository<Procedure> procedures)
    {
        _procedures = procedures;
    }

    public async Task<IReadOnlyCollection<WarrantTemplateStep>> CreateWarrantTemplateStepSequence(
        IEnumerable<WarrantStepDto> warrantStepDtos,
        CancellationToken cancellationToken)
    {

        IReadOnlyCollection<CreateWarrantStepArgs> warrantStepArgs =
            warrantStepDtos
                .Select(x => new CreateWarrantStepArgs(
                    x.ProcedureId,
                    x.CanBeTransitionedToByFrontDesk,
                    x.CanBeTransitionedToByWorkshop))
                .ToList();

        GetProceduresByIdDelegate getProceduresById =
            async ids => await _procedures.ListAsync(new GetProceduresSpecification(ids), cancellationToken);

        IReadOnlyCollection<WarrantTemplateStep> stepSequence =
            await WarrantTemplateStep.CreateStepSequence(warrantStepArgs, getProceduresById);

        return stepSequence;
    }
}
