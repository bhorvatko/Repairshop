using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

internal static class WarrantModelExtensions
{
    public static WarrantSummaryViewModel ToViewModel(this WarrantModel model) =>
        WarrantSummaryViewModel.Create(
            model.Id,
            model.IsUrgent,
            model.Deadline,
            model.Procedure.ToViewModel(),
            model.Title,
            model.CanBeRolledBack,
            model.CanBeAdvanced);
}
