using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.Warrants;

public class UpdateWarrantTest
    : IntegrationTestBase
{
    public UpdateWarrantTest(
        ITestOutputHelper testOutputHelper, 
        DatabaseFixture databaseFixture) 
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Updating_a_warrant()
    {
        // Arrange
        Warrant warrant = await WarrantHelper.CreateAndAddWarrantToDbContext(_dbContext);

        int originalNumberOfSteps = warrant.Steps.Count();

        UpdateWarrantRequest request = new()
        {
            Id = warrant.Id,
            Title = warrant.Title + "aasdfasd",
            IsUrgent = !warrant.IsUrgent,
            Number = warrant.Number + 1,
            Deadline = warrant.Deadline.AddYears(1),
            Steps = warrant.Steps.Take(2).Select(x => new WarrantStepDto()
            {
                ProcedureId = x.ProcedureId,
                CanBeTransitionedToByFrontDesk = true,
                CanBeTransitionedToByWorkshop = true
            }),
            CurrentStepProcedureId = warrant.Steps.ElementAt(1).ProcedureId
        };

        // Act
        await _client.PutAsJsonAsync("Warrants", request);

        // Assert
        Warrant updatedWarrant =
            _dbContext
                .Set<Warrant>()
                .AsNoTracking()
                .Include(x => x.Steps)
                .Include(x => x.CurrentStep)
                .Single();

        updatedWarrant.Should().Match<Warrant>(x =>
            x.Title == request.Title
                && x.Id == request.Id
                && x.IsUrgent == request.IsUrgent
                && x.Number == request.Number
                && x.Deadline == request.Deadline
                && x.Steps.Count() == request.Steps.Count()
                && x.CurrentStep!.ProcedureId == request.CurrentStepProcedureId);
    }

    [Fact]
    public async Task Updating_a_warrant_removes_the_previous_step_sequence()
    {
        // Arrange
        Warrant warrant = await WarrantHelper.CreateAndAddWarrantToDbContext(_dbContext);

        UpdateWarrantRequest request = new()
        {
            Id = warrant.Id,
            Title = warrant.Title,
            IsUrgent = warrant.IsUrgent,
            Deadline = warrant.Deadline,
            Number = warrant.Number,
            Steps = warrant.Steps.Take(warrant.Steps.Count() - 1).Select(x => new WarrantStepDto()
            {
                ProcedureId = x.ProcedureId,
                CanBeTransitionedToByFrontDesk = true,
                CanBeTransitionedToByWorkshop = true
            })
        };

        // Act
        await _client.PutAsJsonAsync("Warrants", request);

        // Assert
        IEnumerable<WarrantStep> savedSteps =
            _dbContext.Set<WarrantStep>().AsNoTracking().ToList();

        savedSteps.Should().HaveCount(2);
    }
}
