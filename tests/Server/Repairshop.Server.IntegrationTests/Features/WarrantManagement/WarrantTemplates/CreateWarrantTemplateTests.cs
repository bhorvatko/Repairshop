using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.WarrantTemplates;

public class CreateWarrantTemplateTests : IntegrationTestBase
{
    public CreateWarrantTemplateTests(
        ITestOutputHelper testOutputHelper,
        DatabaseFixture databaseFixture)
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Creating_a_warrant_template_should_result_in_new_entity()
    {
        // Arrange
        Procedure firstProcedure = ProcedureHelper.Create();
        Procedure secondProcedure = ProcedureHelper.Create();

        _dbContext.AddRange(new[] { firstProcedure, secondProcedure });
        _dbContext.SaveChanges();

        CreateWarrantTemplateRequest request = new()
        {
            Name = "Template",
            Steps = new[]
            {
                new WarrantStepDto()
                {
                    ProcedureId = firstProcedure.Id,
                    CanBeTransitionedToByFrontDesk = false,
                    CanBeTransitionedToByWorkshop = false,
                },
                new WarrantStepDto()
                {
                    ProcedureId = secondProcedure.Id,
                    CanBeTransitionedToByFrontDesk = true,
                    CanBeTransitionedToByWorkshop = true,
                }
            }
        };

        // Act
        await _client.PostAsJsonAsync("WarrantTemplates", request);

        // Assert
        WarrantTemplate createdEntity = _dbContext
            .Set<WarrantTemplate>()
            .Include(x => x.Steps).ThenInclude(x => x.Procedure)
            .Single();

        createdEntity.Name.Should().Be("Template");
        createdEntity.Steps.Should().HaveCount(2);
        createdEntity.Steps
            .OrderByDescending(x => x.CanBeTransitionedToByFrontOffice)
            .Should()
            .SatisfyRespectively(
                firstStep =>
                {
                    firstStep.Procedure.Id.Should().Be(firstProcedure.Id);
                    firstStep.Procedure.Color.Value.Should().Be(firstProcedure.Color);
                    firstStep.CanBeTransitionedToByFrontOffice.Should().BeFalse();
                    firstStep.CanBeTransitionedToByWorkshop.Should().BeFalse();
                },
                secondStep =>
                {
                    secondStep.Procedure.Id.Should().Be(secondProcedure.Id);
                    secondStep.Procedure.Color.Value.Should().Be(secondProcedure.Color);
                    secondStep.CanBeTransitionedToByFrontOffice.Should().BeTrue();
                    secondStep.CanBeTransitionedToByWorkshop.Should().BeTrue();
                });
    }
}
