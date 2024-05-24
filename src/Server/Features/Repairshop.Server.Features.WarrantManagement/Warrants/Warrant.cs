using Repairshop.Server.Common.Entities;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Features.WarrantManagement.Technicians;
using Repairshop.Server.Features.WarrantManagement.Warrants.CreateWarrant;
using Repairshop.Server.Features.WarrantManagement.Warrants.ProcedureChanged;
using Repairshop.Shared.Common.ClientContext;

namespace Repairshop.Server.Features.WarrantManagement.Warrants;
public class Warrant
    : EntityBase
{
#nullable disable
    private Warrant() { }
#nullable enable

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public DateTime Deadline { get; private set; }
    public bool IsUrgent { get; private set; }
    public IEnumerable<WarrantStep> Steps { get; private set; }
    public Guid? CurrentStepId { get; private set; }
    public WarrantStep CurrentStep { get; private set; }
    public Guid? TechnicianId { get; private set; }
    public Technician? Technician { get; private set; }

    public static async Task<Warrant> Create(
        string title,
        DateTime deadline,
        bool isUrgent,
        IEnumerable<WarrantStep> steps,
        Func<Warrant, Task> beforeFinalising)
    {
        Warrant warrant = new Warrant()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Deadline = deadline,
            IsUrgent = isUrgent,
            Steps = steps
        };

        await beforeFinalising(warrant);

        warrant.SetInitialStep();
        warrant.AddEvent(WarrantCreatedEvent.Create(warrant));

        return warrant;
    }

    public void AdvanceToStep(
        Guid nextStepId,
        string clientContext)
    {
        if (CurrentStep is null)
        {
            throw new DomainInvalidOperationException("The warrant does not have it's current step set.");
        }

        if (CurrentStep.NextStep?.Id != nextStepId)
        {
            throw new DomainArgumentException(
                nextStepId,
                "Cannot advance to the specified step.");
        }

        EnsureCanBeTransitioned(clientContext, CurrentStep.NextTransition!);

        SetCurrentStep(CurrentStep.NextStep);
    }

    public void RollbackToStep(
        Guid previousStepId,
        string clientContext)
    {
        if (CurrentStep is null)
        {
            throw new DomainInvalidOperationException("The warrant does not have it's current step set.");
        }

        if (CurrentStep.PreviousStep?.Id != previousStepId)
        {
            throw new DomainArgumentException(
                previousStepId,
                "Cannot rollback to the specified step.");
        }

        EnsureCanBeTransitioned(clientContext, CurrentStep.PreviousTransition!);

        SetCurrentStep(CurrentStep.PreviousStep);
    }

    public async Task Update(
        string title,
        DateTime deadline,
        bool isUrgent,
        IEnumerable<WarrantStep> steps,
        Guid? currentStepProcedureId,
        Func<Task> beforeFinalising)
    {
        Title = title;
        Deadline = deadline;
        IsUrgent = isUrgent;
        Steps = steps;
        CurrentStep = null!;

        await beforeFinalising();

        SetCurrentStepByProcedureId(currentStepProcedureId);
    }

    public void UnassignWarrant()
    {
        TechnicianId = null;
        Technician = null;
    }

    public void AssignTo(Technician technician)
    {
        Technician = technician;
        TechnicianId = technician.Id;
    }

    public IEnumerable<WarrantStep> GetStepsInSequence()
    {
        IEnumerable<WarrantStep> stepsInSequence = 
            Enumerable.Empty<WarrantStep>();

        WarrantStep? currentStep = GetInitialStep();

        while (currentStep is not null)
        {
            stepsInSequence = stepsInSequence.Append(currentStep);

            currentStep = currentStep.NextStep;
        }

        return stepsInSequence;
    }

    private void SetInitialStep() => SetCurrentStep(Steps.First());

    private void SetCurrentStep(WarrantStep step)
    {
        Guid? previousProcedureId = CurrentStep?.ProcedureId;

        if (!Steps.Contains(step))
        {
            throw new DomainArgumentException(
                step,
                "The specified step is not part of the warrant's step sequence.");
        }

        CurrentStep = step;
        CurrentStepId = step.Id;

        if (previousProcedureId is not null 
            && previousProcedureId != CurrentStep.ProcedureId)
        {
            AddEvent(WarrantProcedureChangedEvent.Create(this));
        }
    }

    private void SetCurrentStepByProcedureId(Guid? procedureId)
    {
        if (procedureId is null)
        {
            SetInitialStep();

            return;
        }

        WarrantStep? newCurrentStep =
            Steps.FirstOrDefault(x => x.ProcedureId == procedureId);

        if (newCurrentStep is null)
        {
            throw new DomainArgumentException(
                procedureId,
                $"The warrant sequence does not contain any steps with the procedure ID {procedureId}");
        }

        SetCurrentStep(newCurrentStep);
    }

    private void EnsureCanBeTransitioned(
        string clientContext,
        WarrantStepTransition transition)
    {
        if (clientContext == RepairshopClientContext.FrontOffice
            && !transition.CanBePerformedByFrontOffice)
        {
            throw new DomainInvalidOperationException("The transition cannot be performed by the front office.");
        }

        if (clientContext == RepairshopClientContext.Workshop
            && !transition.CanBePerformedByWorkshop)
        {
            throw new DomainInvalidOperationException("The transition cannot be performed by the workshop.");
        }
    }

    private WarrantStep? GetInitialStep() =>
        Steps.FirstOrDefault(s => s.PreviousTransition is null);
}
