using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Server.Tests.Shared.Features.WarrantManagement;
public static class WarrantTemplateHelper
{
    public async static Task<WarrantTemplate> Create(
        string name = "Name",
        IEnumerable<WarrantTemplateStep>? steps = null) =>
        WarrantTemplate.Create(
            name,
            steps ?? await WarrantTemplateStepHelper.CreateStepSequence(3));
}
