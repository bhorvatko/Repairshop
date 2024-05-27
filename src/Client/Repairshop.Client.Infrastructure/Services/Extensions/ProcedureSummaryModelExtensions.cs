using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Infrastructure.Services.Extensions;

internal static class ProcedureSummaryModelExtensions
{
    public static ProcedureSummaryViewModel ToViewModel(this ProcedureSummaryModel model) =>
        ProcedureSummaryViewModel.Create(model.Id, model.Name, model.Color, model.Priority);
}
