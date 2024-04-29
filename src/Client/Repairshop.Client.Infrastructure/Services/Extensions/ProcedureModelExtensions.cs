using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Shared.Features.WarrantManagement.Procedures;

internal static class ProcedureModelExtensions
{
    public static ProcedureViewModel ToViewModel(this ProcedureModel model) =>
        ProcedureViewModel.Create(
            model.Id,
            model.Name,
            model.Color,
            model.UsedByWarrants,
            model.UsedByWarrantTemplates);
}
