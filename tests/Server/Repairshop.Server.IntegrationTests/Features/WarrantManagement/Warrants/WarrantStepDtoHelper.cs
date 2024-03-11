using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.Warrants;
internal static class WarrantStepDtoHelper
{
    public static WarrantStepDto Create(
        Guid procedureId,
        bool canBeTransitionedToByFrontDesk = true,
        bool canBeTransitionedToByWorkshop = true) =>
        new WarrantStepDto()
        {
            ProcedureId = procedureId,
            CanBeTransitionedToByFrontDesk = canBeTransitionedToByFrontDesk,
            CanBeTransitionedToByWorkshop = canBeTransitionedToByWorkshop
        };
}
