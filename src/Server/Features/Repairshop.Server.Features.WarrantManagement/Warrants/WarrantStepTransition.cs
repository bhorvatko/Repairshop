using Repairshop.Server.Common.Exceptions;

namespace Repairshop.Server.Features.WarrantManagement.Warrants;

public class WarrantStepTransition
{
#nullable disable
    private WarrantStepTransition() { }
#nullable enable

    public Guid Id { get; private set; }
    public Guid PreviousStepId { get; private set; }
    public WarrantStep PreviousStep { get; private set; }
    public Guid NextStepId { get; private set; }
    public WarrantStep NextStep { get; private set; }
    public bool CanBePerformedByFrontOffice { get; private set; }
    public bool CanBePerformedByWorkshop { get; private set; }

    public static WarrantStepTransition Create(
        WarrantStep fromStep,
        WarrantStep toStep,
        bool canBePerformedByFrontOffice,
        bool canBePerformedByWorkshop)
    {
        if (fromStep == toStep)
        {
            throw new DomainArgumentException(
                toStep,
                "The from and to steps of a transition cannot be the same step");
        }

        return new WarrantStepTransition()
        {
            Id = Guid.NewGuid(),
            PreviousStepId = fromStep.Id,
            PreviousStep = fromStep,
            NextStepId = toStep.Id,
            NextStep = toStep,
            CanBePerformedByFrontOffice = canBePerformedByFrontOffice,
            CanBePerformedByWorkshop = canBePerformedByWorkshop
        };
    }
}
