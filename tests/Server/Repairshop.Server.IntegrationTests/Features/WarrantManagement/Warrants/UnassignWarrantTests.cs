using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repairshop.Server.Features.WarrantManagement.Technicians;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.Warrants;

public class UnassignWarrantTests
    : IntegrationTestBase
{
    public UnassignWarrantTests(
        ITestOutputHelper testOutputHelper, 
        DatabaseFixture databaseFixture) 
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Unassigning_a_warrant_from_the_technician()
    {
        // Arrange
        Warrant warrant = await WarrantHelper.Create();

        Technician technician = TechnicianHelper.Create();

        technician.AssignWarrant(warrant);

        _dbContext.Add(technician);
        _dbContext.SaveChanges();

        UnassignWarrantRequest request = new()
        {
            Id = warrant.Id
        };

        // Act
        await _client.PutAsJsonAsync($"Warrants/{request.Id}/Unassign", request);

        // Assert
        Technician resultingTechnician = _dbContext
            .Set<Technician>()
            .AsNoTracking()
            .Include(x => x.Warrants)
            .Single();

        resultingTechnician.Warrants.Should().BeEmpty();

        Warrant resultingWarrant = _dbContext
            .Set<Warrant>()
            .AsNoTracking()
            .Single();

        resultingWarrant.TechnicianId.Should().BeNull();
    }
}
