﻿using Repairshop.Server.Common.Entities;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Features.WarrantManagement.Technicians;

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
    public WarrantStep? CurrentStep { get; private set; }
    public Guid? TechnicianId { get; private set; }
    public Technician? Technician { get; private set; }

    public static Warrant Create(
        string title,
        DateTime deadline,
        bool isUrgent,
        IEnumerable<WarrantStep> steps)
    {
        Warrant warrant = new Warrant()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Deadline = deadline,
            IsUrgent = isUrgent,
            Steps = steps
        };

        warrant.AddEvent(WarrantCreatedEvent.Create(warrant));

        return warrant;
    }

    public void SetInitialStep() => SetCurrentStep(Steps.First());

    public void AdvanceToStep(Guid nextStepId)
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

        SetCurrentStep(CurrentStep.NextStep);
    }

    private void SetCurrentStep(WarrantStep step)
    {
        CurrentStep = step;
        CurrentStepId = step.Id;
    }
}
