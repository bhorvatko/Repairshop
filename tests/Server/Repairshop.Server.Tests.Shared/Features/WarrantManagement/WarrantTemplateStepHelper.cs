using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;
using static Repairshop.Server.Features.WarrantManagement.WarrantTemplates.WarrantTemplateStep;

namespace Repairshop.Server.Tests.Shared.Features.WarrantManagement;
internal static class WarrantTemplateStepHelper
{
    public static async Task<IEnumerable<WarrantTemplateStep>> CreateStepSequence(
        int numberOfSteps,
        bool canBeTransitionedByFrontOffice = true,
        bool canBeTransitionedByWorkshop = true)
    {
        IEnumerable<Procedure> procedures = ProcedureHelper.Create(numberOfSteps);

        IEnumerable<CreateWarrantStepArgs> stepArgs =
            procedures.Select(x =>
                new CreateWarrantStepArgs(
                    x.Id,
                    canBeTransitionedByFrontOffice,
                    canBeTransitionedByWorkshop));

        GetProceduresByIdDelegate getProceduresById =
            ids => Task.FromResult(procedures.Where(p => ids.Contains(p.Id)));

        return await WarrantTemplateStep.CreateStepSequence(stepArgs, getProceduresById);
    }
}
