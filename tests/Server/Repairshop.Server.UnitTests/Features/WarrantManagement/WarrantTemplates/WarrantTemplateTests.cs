using FluentAssertions;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;

namespace Repairshop.Server.UnitTests.Features.WarrantManagement.WarrantTemplates;
public class WarrantTemplateTests
{
    [Fact]
    public async Task Creating_a_warrant_template()
    {
        string name = "name";
        IEnumerable<WarrantTemplateStep> stepSequence = 
            await WarrantTemplateStepHelper.CreateStepSequence(3);

        WarrantTemplate warrantTemplate = WarrantTemplate.Create(
            name,
            stepSequence);

        warrantTemplate.Name.Should().Be(name);
        warrantTemplate.Steps.Select(x => x.Id).Should().BeEquivalentTo(stepSequence.Select(x => x.Id));
    }

    [Fact]
    public async Task Updating_a_warrant_template()
    {
        string updatedName = "updated name";
        IEnumerable<WarrantTemplateStep> updatedStepSequence = 
            await WarrantTemplateStepHelper.CreateStepSequence(4);
        WarrantTemplate warrantTemplate = await WarrantTemplateHelper.Create();

        warrantTemplate.Update(
            updatedName,
            updatedStepSequence);

        warrantTemplate.Name.Should().Be(updatedName);
        warrantTemplate.Steps.Select(x => x.Id).Should().BeEquivalentTo(updatedStepSequence.Select(x => x.Id));
    }
}
