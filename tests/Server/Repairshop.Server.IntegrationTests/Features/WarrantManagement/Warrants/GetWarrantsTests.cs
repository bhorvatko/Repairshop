﻿using FluentAssertions;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.Technicians;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.IntegrationTests.Common;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Common.ClientContext;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Features.WarrantManagement.Warrants;

public class GetWarrantsTests
    : IntegrationTestBase
{
    public GetWarrantsTests(
        ITestOutputHelper testOutputHelper, 
        DatabaseFixture databaseFixture) 
        : base(testOutputHelper, databaseFixture)
    {
    }

    [Fact]
    public async Task Getting_warrants_assigned_to_a_technician()
    {
        // Arrange
        Technician technician = TechnicianHelper.Create();
        Warrant assignedWarrant = await WarrantHelper.Create();

        technician.AssignWarrant(assignedWarrant);

        _dbContext.Add(technician);
        _dbContext.SaveChanges();

        assignedWarrant.SetInitialStep();

        _dbContext.SaveChanges();

        GetWarrantsRequest request = new()
        {
            TechnicianId = technician.Id
        };

        // Act
        GetWarrantsResponse response =
            (await _client.GetFromJsonAsync<GetWarrantsResponse>("Warrants", request))!;

        // Assert
        response.Warrants.Should().HaveCount(1);
        response.Warrants.Single().Should().Match<WarrantModel>(x =>
            x.TechnicianId == technician.Id
                && x.Id == assignedWarrant.Id
                && x.IsUrgent == assignedWarrant.IsUrgent
                && x.Deadline == assignedWarrant.Deadline);
        response.Warrants.Single().Procedure.Should().Match<ProcedureModel>(x =>
            x.Id == assignedWarrant.CurrentStep!.ProcedureId
                && x.Color == assignedWarrant.CurrentStep!.Procedure.Color
                && x.Name == assignedWarrant.CurrentStep!.Procedure.Name);
    }

    [Fact]
    public async Task Getting_unassigned_warrants()
    {
        // Arrange
        Warrant warrant = await WarrantHelper.Create();

        _dbContext.Add(warrant);
        _dbContext.SaveChanges();

        warrant.SetInitialStep();

        _dbContext.SaveChanges();

        GetWarrantsRequest request = new()
        {
            TechnicianId = null
        };

        // Act
        GetWarrantsResponse response =
            (await _client.GetFromJsonAsync<GetWarrantsResponse>("Warrants", request))!;

        // Assert
        response.Warrants.Should().HaveCount(1);
        response.Warrants.Single().Should().Match<WarrantModel>(x =>
            x.TechnicianId == null
                && x.Id == warrant.Id
                && x.IsUrgent == warrant.IsUrgent
                && x.Deadline == warrant.Deadline);
        response.Warrants.Single().Procedure.Should().Match<ProcedureModel>(x =>
            x.Id == warrant.CurrentStep!.ProcedureId
                && x.Color == warrant.CurrentStep!.Procedure.Color
                && x.Name == warrant.CurrentStep!.Procedure.Name);
    }

    [Theory]
    [InlineData(RepairshopClientContext.FrontOffice, true, false)]
    [InlineData(RepairshopClientContext.Workshop, false, true)]
    public async Task Can_be_advanced_and_rollbacked_are_correctly_resolved_for_the_current_client_context(
        string clientContext,
        bool secondStepCanBeTransitionedToByFrontOffice,
        bool secondStepCanBeTransitionedToByWorkshop)
    {
        // Arrange
        _client.AddClientContextHeader(clientContext);

        Procedure firstProcedure = ProcedureHelper.Create();
        Procedure secondProcedure = ProcedureHelper.Create();

        IEnumerable<CreateWarrantStepArgs> createWarrantsStepArgs = new List<CreateWarrantStepArgs>()
        {
            new(firstProcedure.Id, false, false),
            new(secondProcedure.Id, secondStepCanBeTransitionedToByFrontOffice, secondStepCanBeTransitionedToByWorkshop)
        };

        IEnumerable<WarrantStep> stepSequence =
            await WarrantStep.CreateStepSequence(
                createWarrantsStepArgs,
                _ => Task.FromResult(new[] { firstProcedure, secondProcedure }.AsEnumerable()));

        Warrant warrant = await WarrantHelper.Create(steps: stepSequence);

        _dbContext.Add(warrant);
        _dbContext.SaveChanges();

        warrant.SetInitialStep();

        _dbContext.SaveChanges();

        GetWarrantsRequest request = new()
        {
            TechnicianId = null
        };

        // Act
        GetWarrantsResponse response =
            (await _client.GetFromJsonAsync<GetWarrantsResponse>("Warrants", request))!;

        // Assert
        response.Warrants.Single().Should().Match<WarrantModel>(x =>
            x.CanBeRolledBack == false && x.CanBeAdvanced == true);
    }
}