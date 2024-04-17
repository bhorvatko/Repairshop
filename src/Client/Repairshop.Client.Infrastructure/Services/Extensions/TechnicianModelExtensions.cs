using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Shared.Features.WarrantManagement.Technicians;

internal static class TechnicianModelExtensions
{
    public static TechnicianViewModel ToViewModel(this TechnicianModel model) =>
        TechnicianViewModel.Create(
            model.Id,
            model.Name,
            model.Warrants.Select(w => w.ToViewModel()));
}
