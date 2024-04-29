using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;
using static Repairshop.Server.Features.WarrantManagement.WarrantTemplates.WarrantTemplateStep;

namespace Repairshop.Server.Tests.Shared.Features.WarrantManagement;

public static class WarrantTemplateStepHelper
{
    public static Task<IEnumerable<WarrantTemplateStep>> CreateStepSequence(
        int numberOfSteps,
        bool canBeTransitionedByFrontOffice = true,
        bool canBeTransitionedByWorkshop = true) =>
        CreateStepSequence(
            ProcedureHelper.Create(numberOfSteps),
            canBeTransitionedByFrontOffice,
            canBeTransitionedByWorkshop);

    public static async Task<IEnumerable<WarrantTemplateStep>> CreateStepSequence(
        IEnumerable<Procedure> procedures,
        bool canBeTransitionedByFrontOffice = true,
        bool canBeTransitionedByWorkshop = true)
    {

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
