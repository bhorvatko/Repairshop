using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using static Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStep;

namespace Repairshop.Server.Tests.Shared.Features.WarrantManagement;
public static class WarrantStepHelper
{
    public static async Task<IEnumerable<WarrantStep>> CreateStepSequence(int numberOfSteps)
    {
        IEnumerable<Procedure> procedures = ProcedureHelper.Create(numberOfSteps);

        IEnumerable<CreateWarrantStepArgs> stepArgs =
            procedures.Select(x => new CreateWarrantStepArgs(x.Id, false, false));

        GetProceduresByIdDelegate getProceduresById =
            ids => Task.FromResult(procedures.Where(p => ids.Contains(p.Id)));

        return await WarrantStep.CreateStepSequence(stepArgs, getProceduresById);
    }
}
