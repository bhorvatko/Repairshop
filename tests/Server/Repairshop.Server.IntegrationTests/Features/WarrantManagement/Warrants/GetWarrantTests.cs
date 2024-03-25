﻿using FluentAssertions;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.Warrants;

public class GetWarrantTests
    : IntegrationTestBase
{
    public GetWarrantTests(
        ITestOutputHelper testOutputHelper, 
        DatabaseFixture databaseFixture) 
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Getting_a_warrant()
    {
        // Arrange
        Warrant warrant = await WarrantHelper.Create();
        
        _dbContext.Add(warrant);
        _dbContext.SaveChanges();

        warrant.SetInitialStep();

        _dbContext.SaveChanges();

        GetWarrantRequest request = new() { Id = warrant.Id };

        // Act
        GetWarrantResponse response = 
            (await _client.GetFromJsonAsync<GetWarrantResponse>($"Warrants/{warrant.Id}"))!;

        // Assert
        response.Should().Match<GetWarrantResponse>(x => 
            x.Id == warrant.Id
                && x.IsUrgent == warrant.IsUrgent
                && x.Deadline == warrant.Deadline
                && x.Title == warrant.Title
                && x.WarrantSteps.Count() == warrant.Steps.Count());
    }
}