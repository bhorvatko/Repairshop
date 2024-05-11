using FluentAssertions;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Repairshop.Server.Features.WarrantManagement.Technicians;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Features.WarrantManagement.Technicians;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.Technicians;

public class TechnicianTests
    : IntegrationTestBase
{
    public TechnicianTests(
        ITestOutputHelper testOutputHelper, 
        DatabaseFixture databaseFixture) 
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Creating_a_technician_should_result_in_a_new_entity()
    {
        CreateTechnicianRequest request = new()
        {
            Name = "Test",
        };

        await _client.PostAsJsonAsync("Technicians", request);

        Technician result = _dbContext
            .Set<Technician>()
            .AsNoTracking()
            .Include(x => x.Warrants)
            .Single();

        result.Name.Should().Be("Test");
        result.Warrants.Should().BeEmpty();
    }

    [Fact]
    public async Task Assigning_an_unassigned_warrant()
    {
        // Arrange
        Technician technician = TechnicianHelper.Create();
        _dbContext.Add(technician);

        Warrant warrant = await WarrantHelper.CreateAndAddWarrantToDbContext(_dbContext);

        AssignWarrantRequest request = new()
        {
            WarrantId = warrant.Id,
            TechnicianId = technician.Id
        };

        // Act
        await _client.PutAsJsonAsync("Technicians/AssignWarrant", request);

        // Assert
        Technician result = _dbContext
            .Set<Technician>()
            .AsNoTracking()
            .Include(x => x.Warrants)
            .Single();

        result.Warrants.Should().HaveCount(1);
        result.Warrants.Single().Id.Should().Be(warrant.Id);
    }

    [Fact]
    public async Task Assigning_a_warrant_from_one_technician_to_another()
    {
        // Arrange
        Warrant warrant = await WarrantHelper.CreateAndAddWarrantToDbContext(_dbContext);

        Technician firstTechnician = TechnicianHelper.Create("First");

        Technician secondTechnician = TechnicianHelper.Create("Second");
        secondTechnician.AssignWarrant(warrant);

        _dbContext.AddRange(new[] { firstTechnician, secondTechnician });

        _dbContext.SaveChanges();

        AssignWarrantRequest request = new()
        {
            WarrantId = warrant.Id,
            TechnicianId = secondTechnician.Id
        };

        // Act
        await _client.PutAsJsonAsync("Technicians/AssignWarrant", request);

        // Assert
        Technician firstResultingTechnician = _dbContext
            .Set<Technician>()
            .AsNoTracking()
            .Include(x => x.Warrants)
            .Single(x => x.Id == firstTechnician.Id);

        firstResultingTechnician.Warrants.Should().BeEmpty();

        Technician secondResultingTechnician = _dbContext
            .Set<Technician>()
            .AsNoTracking()
            .Include(x => x.Warrants)
            .Single(x => x.Id == secondTechnician.Id);

        secondResultingTechnician.Warrants.Should().HaveCount(1);
    }

    [Fact]
    public async Task Assigning_a_warrant_should_create_a_notification()
    {
        // Arrange
        Technician technician = TechnicianHelper.Create();
        _dbContext.Add(technician);

        Warrant warrant = await WarrantHelper.CreateAndAddWarrantToDbContext(_dbContext);

        AssignWarrantRequest request = new()
        {
            WarrantId = warrant.Id,
            TechnicianId = technician.Id
        };

        WarrantAssignedNotification? notification = null;

        await SubscribeToNotification<WarrantAssignedNotification>(x => notification = x);

        // Act
        await _client.PutAsJsonAsync("Technicians/AssignWarrant", request);

        // TO DO: Find an alternative way of waiting for the notification to be received
        await Task.Delay(100);

        // Assert
        notification.Should().NotBeNull();
        notification!.Warrant.Id.Should().Be(warrant.Id);
        notification!.ToTechnicianId.Should().Be(technician.Id);
        notification!.FromTechnicianId.Should().BeNull();
    }

    [Fact]
    public async Task Getting_technicians()
    {
        // Arrange
        Technician technician = TechnicianHelper.Create();

        _dbContext.Add(technician);
        _dbContext.SaveChanges();

        // Act
        GetTechniciansResponse response =
            (await _client.GetFromJsonAsync<GetTechniciansResponse>("Technicians"))!;

        // Assert
        response.Technicians.Should().HaveCount(1);
        response.Technicians.Single().Should().Match<TechnicianModel>(x => 
            x.Name == technician.Name
                && x.Id == technician.Id);
    }

    [Fact]
    public async Task Updating_a_technician()
    {
        // Arrange
        Technician technician = TechnicianHelper.Create(name: "Original");

        _dbContext.Add(technician);
        _dbContext.SaveChanges();

        UpdateTechnicianRequest request = new()
        {
            TechnicianId = technician.Id,
            Name = "Updated"
        };

        // Act
        await _client.PutAsJsonAsync("Technicians", request);

        // Assert
        Technician updatedTechnician = _dbContext.Set<Technician>().AsNoTracking().Single();

        updatedTechnician.Name.Should().Be("Updated");
    }

    [Fact]
    public async Task Deleting_a_technician()
    {
        // Arrange
        Technician technician = TechnicianHelper.Create();

        Warrant warrant = 
            await WarrantHelper.CreateAndAddWarrantToDbContext(_dbContext);

        _dbContext.Add(technician);
        _dbContext.SaveChanges();

        IReadOnlyCollection<Technician> techniciansBeforeDeleting =
            _dbContext.Set<Technician>().AsNoTracking().ToList();

        // Act
        await _client.DeleteAsync($"Technicians/{technician.Id}");

        // Assert
        IReadOnlyCollection<Technician> techniciansAfterDeleting = 
            _dbContext.Set<Technician>().AsNoTracking().ToList();

        techniciansBeforeDeleting.Should().HaveCount(1);
        techniciansAfterDeleting.Should().BeEmpty();

        Warrant unassignedWarrant =
            _dbContext.Set<Warrant>().AsNoTracking().Single();

        unassignedWarrant.TechnicianId.Should().BeNull();
    }
}
