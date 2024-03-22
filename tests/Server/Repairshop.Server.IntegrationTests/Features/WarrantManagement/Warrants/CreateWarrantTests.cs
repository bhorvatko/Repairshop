using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.Warrants;

public class CreateWarrantTests
    : IntegrationTestBase
{
    public CreateWarrantTests(
        ITestOutputHelper testOutputHelper, 
        DatabaseFixture databaseFixture) 
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Creating_a_warrant_should_result_in_new_entity()
    {
        // Arrange
        Procedure firstProcedure = ProcedureHelper.Create();
        Procedure secondProcedure = ProcedureHelper.Create();

        _dbContext.AddRange(new[] { firstProcedure, secondProcedure });
        _dbContext.SaveChanges();

        CreateWarrantRequest request = new()
        {
            Deadline = new DateTime(2000, 1, 1),
            IsUrgnet = true,
            Title = "Title",
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
        await _client.PostAsJsonAsync("Warrants", request);

        // Assert
        Warrant createdEntity = _dbContext
            .Set<Warrant>()
            .Include(x => x.Steps).ThenInclude(x => x.NextTransition)
            .Include(x => x.Steps).ThenInclude(x => x.PreviousTransition)
            .Single();

        createdEntity.Deadline.Should().Be(new DateTime(2000, 1, 1));
        createdEntity.IsUrgent.Should().BeTrue();
        createdEntity.Title.Should().Be("Title");
        createdEntity.Steps.Should().HaveCount(2);
        createdEntity.CurrentStep!.ProcedureId.Should().Be(firstProcedure.Id);
        createdEntity.Steps.OrderByDescending(x => x.NextTransition is not null).Should().SatisfyRespectively(
            firstStep =>
            {
                firstStep.ProcedureId.Should().Be(firstProcedure.Id);
                firstStep.Procedure.Color.Value.Should().Be(firstProcedure.Color);
                firstStep.PreviousTransition.Should().BeNull();
                firstStep.NextTransition.Should().NotBeNull();
                firstStep.NextTransition!.CanBePerformedByFrontOffice.Should().BeTrue();
                firstStep.NextTransition!.CanBePerformedByWorkshop.Should().BeTrue();
                firstStep.NextTransition.NextStep.Should().NotBeNull();
            },
            secondStep =>
            {
                secondStep.PreviousTransition.Should().NotBeNull();
                secondStep.PreviousTransition!.PreviousStep.Should().NotBeNull();
                secondStep.PreviousTransition.CanBePerformedByFrontOffice.Should().BeTrue();
                secondStep.PreviousTransition!.CanBePerformedByWorkshop.Should().BeTrue();
                secondStep.NextTransition.Should().BeNull();
            });
    }
}
