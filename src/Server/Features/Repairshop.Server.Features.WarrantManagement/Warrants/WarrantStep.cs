using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Warrants;
public class WarrantStep
{
#nullable disable
    private WarrantStep() { }
#nullable enable

    public delegate Task<IEnumerable<Procedure>> GetProceduresByIdDelegate(IEnumerable<Guid> ids);

    public Guid Id { get; private set; }
    public Guid ProcedureId { get; private set; }
    public Procedure Procedure { get; private set; }
    public WarrantStepTransition? NextTransition { get; private set; }
    public WarrantStepTransition? PreviousTransition { get; private set; }
    public Guid WarrantId { get; private set; }

    public WarrantStep? NextStep => NextTransition?.NextStep;

    public static async Task<IEnumerable<WarrantStep>> CreateStepSequence(
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

        IEnumerable<Procedure> procedures =
            await getProcedures(stepArgs.Select(x => x.ProcedureId));

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

        WarrantStep currentStep = CreateFirstStep(getProcedure(stepArgs.First()));

        List<WarrantStep> stepSequence = [currentStep];

        foreach (CreateWarrantStepArgs stepArg in stepArgs.Skip(1))
        {
            currentStep = currentStep
                .CreateNextStep(
                    getProcedure(stepArg),
                    stepArg.CanBeTransitionedToByFrontDesk,
                    stepArg.CanBeTransitionedToByWorkshop);

            stepSequence.Add(currentStep);
        }

        return stepSequence;
    }

    private static WarrantStep CreateFirstStep(Procedure procedure)
    {
        return new WarrantStep()
        {
            Id = Guid.NewGuid(),
            Procedure = procedure,
            ProcedureId = procedure.Id,
        };
    }

    private WarrantStep CreateNextStep(
        Procedure procedure,
        bool canBeTransitionedToByFrontdesk,
        bool canBeTransitionedToByWorkshop)
    {
        WarrantStep nextStep = new WarrantStep()
        {
            Id = Guid.NewGuid(),
            Procedure = procedure,
            ProcedureId = procedure.Id
        };

        WarrantStepTransition transition =
            WarrantStepTransition.Create(
                this,
                nextStep,
                canBeTransitionedToByFrontdesk,
                canBeTransitionedToByWorkshop);

        this.NextTransition = transition;
        nextStep.PreviousTransition = transition;

        return nextStep;
    }
}

public class CreateWarrantStepArgs
{
    public CreateWarrantStepArgs(
        Guid procedureId, 
        bool canBeTransitionedToByFrontDesk, 
        bool canBeTransitionedToByWorkshop)
    {
        ProcedureId = procedureId;
        CanBeTransitionedToByFrontDesk = canBeTransitionedToByFrontDesk;
        CanBeTransitionedToByWorkshop = canBeTransitionedToByWorkshop;
    }

    public Guid ProcedureId { get; private set; }
    public bool CanBeTransitionedToByFrontDesk { get; private set; }
    public bool CanBeTransitionedToByWorkshop { get; private set; }
}
