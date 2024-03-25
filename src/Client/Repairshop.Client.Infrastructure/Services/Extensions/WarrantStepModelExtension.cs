using Repairshop.Client.Features.WarrantManagement.Warrants;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Infrastructure.Services.Extensions;

internal static class WarrantStepModelExtension
{
    public static WarrantStep ToViewModel(this WarrantStepModel model) =>
        WarrantStep.Create(
            model.Procedure.ToViewModel(),
            model.CanBeTransitionedToByFrontOffice,
            model.CanBeTransitionedToByWorkshop);
}
