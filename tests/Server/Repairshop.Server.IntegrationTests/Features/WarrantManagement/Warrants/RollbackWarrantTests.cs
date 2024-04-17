﻿using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
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
        Warrant warrant = await WarrantHelper.Create();

        _dbContext.Add(warrant);
        _dbContext.SaveChanges();

        warrant.SetInitialStep();
        warrant.AdvanceToStep(warrant.Steps.ElementAt(1).Id);

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
}