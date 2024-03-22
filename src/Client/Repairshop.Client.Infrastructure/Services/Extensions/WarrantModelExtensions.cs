using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

internal static class WarrantModelExtensions
{
    public static WarrantViewModel ToViewModel(this WarrantModel model) =>
        WarrantViewModel.Create(
            model.IsUrgent,
            model.Deadline,
            model.Procedure.ToViewModel(),
            model.Title,
            model.CanBeAdvanced,
            model.CanBeRolledBack);
}
