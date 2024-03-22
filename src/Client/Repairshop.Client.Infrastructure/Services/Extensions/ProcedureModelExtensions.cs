using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Shared.Features.WarrantManagement.Procedures;

internal static class ProcedureModelExtensions
{
    public static Procedure ToViewModel(this ProcedureModel model) =>
        Procedure.Create(model.Id, model.Name, model.Color);

}
