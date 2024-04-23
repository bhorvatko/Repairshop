using Repairshop.Server.Common.Entities;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.WarrantTemplates;

public class WarrantTemplateStep
    : EntityBase
{
#nullable disable
    private WarrantTemplateStep() { }
#nullable enable

    public delegate Task<IEnumerable<Procedure>> GetProceduresByIdDelegate(IEnumerable<Guid> ids);

    public Guid Id { get; private set; }
    public Procedure Procedure { get; private set; }
    public bool CanBeTransitionedToByWorkshop { get; private set; }
    public bool CanBeTransitionedToByFrontOffice { get; private set; }
    public Guid WarrantTemplateId { get; private set; }

    public static async Task<IReadOnlyCollection<WarrantTemplateStep>> CreateStepSequence(
        IEnumerable<CreateWarrantStepArgs> stepArgs,
        GetProceduresByIdDelegate getProcedures)
    {
        if (!stepArgs.Any())
        {
            throw new DomainArgumentException(
                stepArgs,
                "Step sequence must contain at least one step.");
        }

        if (stepArgs.GroupBy(x => x.ProcedureId).Any(g => g.Count() > 1))
        {
            throw new DomainArgumentException(
                stepArgs,
                "A procedure cannot occur more than once in a step sequence.");
        }

        if (stepArgs.Skip(1).Any(x => !x.CanBeTransitionedToByFrontDesk && !x.CanBeTransitionedToByWorkshop))
        {
            throw new DomainArgumentException(
                stepArgs,
                "All steps (except for the first) must be advancable to by at least the front desk or workshop");
        }

        IReadOnlyCollection<Procedure> procedures =
            (await getProcedures(stepArgs.Select(x => x.ProcedureId))).ToList();

        Func<CreateWarrantStepArgs, Procedure> getProcedure = stepArgs =>
        {
            Procedure? procedure =
                procedures.FirstOrDefault(x => x.Id == stepArgs.ProcedureId);

            if (procedure is null)
            {
                throw new EntityNotFoundException<Procedure, Guid>(stepArgs.ProcedureId);
            }

            return procedure;
        };

        var result = stepArgs.Select(x =>
            new WarrantTemplateStep()
            {
                Id = Guid.NewGuid(),
                Procedure = getProcedure(x),
                CanBeTransitionedToByWorkshop = x.CanBeTransitionedToByWorkshop,
                CanBeTransitionedToByFrontOffice = x.CanBeTransitionedToByFrontDesk
            })
            .ToList();

        return result;
    }
}
