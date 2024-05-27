using Repairshop.Server.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Tests.Shared.Features.WarrantManagement;

public static class ProcedureHelper
{
    public static Procedure Create(
        string name = "New Procedure",
        string color = "FFFFFF") =>
        Procedure.Create(
            name, 
            ColorCode.FromHexCode(color),
            ProcedurePriority.FromFloating(1));

    public static IEnumerable<Procedure> Create(int numberOfProcedures) =>
        Enumerable
            .Range(0, numberOfProcedures)
            .Select(_ => Create())
            .ToList();
}
