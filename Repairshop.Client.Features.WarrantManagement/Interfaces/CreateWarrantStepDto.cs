using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Features.WarrantManagement.Interfaces;

public class CreateWarrantStepDto
{
    public required Guid ProcedureId { get; set; }
    public required bool CanBeTransitionedToByFrontDesk { get; set; }
    public required bool CanBeTransitionedToByWorkshop { get; set; }

    public WarrantStepDto ToWarrantStepDto() =>
        new WarrantStepDto()
        {
            CanBeTransitionedToByFrontDesk = CanBeTransitionedToByFrontDesk,
            CanBeTransitionedToByWorkshop = CanBeTransitionedToByWorkshop,
            ProcedureId = ProcedureId
        };
}
