using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Common.ClientContext;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.Warrants;

public class RollbackWarrantTests
    : IntegrationTestBase
{
    public RollbackWarrantTests(
        ITestOutputHelper testOutputHelper, 
        DatabaseFixture databaseFixture) 
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Rolling_back_a_warrant_to_the_next_step()
    {
        // Arrange
        Warrant warrant = await WarrantHelper.CreateAndAddWarrantToDbContext(_dbContext);

        warrant.AdvanceToStep(warrant.Steps.ElementAt(1).Id, RepairshopClientContext.FrontOffice);

        _dbContext.SaveChanges();

        RollbackWarrantRequest request = new RollbackWarrantRequest
        {
            WarrantId = warrant.Id,
            StepId = warrant.Steps.First().Id
        };

        // Act
        await _client.PostAsJsonAsync("Warrants/Rollback", request);

        // Assert
        Warrant result = _dbContext
            .Set<Warrant>()
            .AsNoTracking()
            .Include(x => x.CurrentStep)
            .Single();

        result.CurrentStep!.Id.Should().Be(warrant.Steps.First().Id);
    }

    [Fact]
    public async Task Rolling_back_a_warrant_should_send_a_notification()
    {
        // Arrange
        Warrant warrant = await WarrantHelper.CreateAndAddWarrantToDbContext(_dbContext);

        warrant.AdvanceToStep(warrant.Steps.ElementAt(1).Id, RepairshopClientContext.FrontOffice);

        _dbContext.SaveChanges();

        RollbackWarrantRequest request = new RollbackWarrantRequest
        {
            WarrantId = warrant.Id,
            StepId = warrant.Steps.First().Id
        };

        WarrantProcedureChangedNotification? notification = null;

        await SubscribeToNotification<WarrantProcedureChangedNotification>(x => notification = x);

        // Act
        await _client.PostAsJsonAsync("Warrants/Rollback", request);
        await Task.Delay(100);

        // Assert
        notification.Should().NotBeNull();
        notification!.Warrant.Id.Should().Be(warrant.Id);
    }
}
