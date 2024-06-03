using FluentAssertions;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Features.WarrantManagement.Technicians;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using Repairshop.Shared.Common.ClientContext;

namespace Repairshop.Server.UnitTests.Features.WarrantManagement.Warrants;

public class WarrantTests
{
    [Fact]
    public async Task Creating_a_valid_warrant()
    {
        string title = "Title";
        DateTime deadline = new DateTime(2000, 1, 1);
        bool isUrgent = true;
        int number = 1;
        IEnumerable<WarrantStep> steps = await WarrantStepHelper.CreateStepSequence(3);

        Warrant warrant = await Warrant.Create(
            title,
            deadline,
            isUrgent,
            number,
            steps,
            GetUtcNow,
            _ => Task.CompletedTask);

        warrant.Title.Should().Be(title);
        warrant.Deadline.Should().Be(deadline);
        warrant.IsUrgent.Should().Be(isUrgent);
        warrant.Steps.Count().Should().Be(steps.Count());
        warrant.Number.Should().Be(number);
    }

    [Fact]
    public async Task Advancing_a_warrant_to_the_next_step()
    {
        IEnumerable<WarrantStep> steps = await WarrantStepHelper.CreateStepSequence(3);
        Warrant warrant = await WarrantHelper.Create(steps: steps);

        warrant.AdvanceToStep(
            steps.Skip(1).First().Id, 
            RepairshopClientContext.FrontOffice,
            GetUtcNow);

        warrant.CurrentStep.Should().Be(steps.Skip(1).First());
    }

    [Fact]
    public async Task Advancing_a_warrant_to_a_step_which_is_not_next_in_the_sequence_should_fail()
    {
        IEnumerable<WarrantStep> steps = await WarrantStepHelper.CreateStepSequence(3);
        Warrant warrant = await WarrantHelper.Create(steps: steps);

        Action act = 
            () => warrant.AdvanceToStep(steps.Last().Id, RepairshopClientContext.FrontOffice, GetUtcNow);

        act.Should().Throw<DomainArgumentException>();
    }

    [Fact]
    public async Task Rollback_a_warrant_to_the_previous_step()
    {
        IEnumerable<WarrantStep> steps = await WarrantStepHelper.CreateStepSequence(3);
        Warrant warrant = await WarrantHelper.Create(steps: steps);
        warrant.AdvanceToStep(steps.Select(x => x.Id).ElementAt(1), RepairshopClientContext.FrontOffice, GetUtcNow);

        warrant.RollbackToStep(steps.First().Id, RepairshopClientContext.FrontOffice, GetUtcNow);

        warrant.CurrentStep.Should().Be(steps.First());
    }

    [Fact]
    public async Task Rollback_a_warrant_to_a_step_which_is_not_previous_in_the_sequence_should_fail()
    {
        IEnumerable<WarrantStep> steps = await WarrantStepHelper.CreateStepSequence(3);
        Warrant warrant = await WarrantHelper.Create(steps: steps);
        warrant.AdvanceToStep(steps.Select(x => x.Id).ElementAt(1), RepairshopClientContext.FrontOffice, GetUtcNow);

        Action act = () => warrant.RollbackToStep(steps.Last().Id, RepairshopClientContext.FrontOffice, GetUtcNow);

        act.Should().Throw<DomainArgumentException>();
    }

    [Fact]
    public async Task Updating_a_warrant()
    {
        // Arrange
        Warrant warrant = await WarrantHelper.Create();

        string updatedTitle = "Updated title";
        DateTime updatedDeadline = warrant.Deadline.AddYears(1);
        bool updatedIsUrgent = !warrant.IsUrgent;
        int updatedNumber = 2;
        IEnumerable<WarrantStep> updatedSteps = warrant.Steps.Take(2);

        // Act
        await warrant.Update(
            updatedTitle,
            updatedDeadline,
            updatedIsUrgent,
            updatedNumber,
            updatedSteps,
            null,
            () => Task.CompletedTask);

        // Assert
        warrant.Should().Match<Warrant>(x =>
            x.Title == updatedTitle
                && x.Deadline == updatedDeadline
                && x.IsUrgent == updatedIsUrgent
                && x.Number == updatedNumber
                && x.Steps.Count() == updatedSteps.Count());
    }

    [Fact]
    public async Task Unassigning_a_warrant()
    {
        Warrant warrant = await WarrantHelper.Create();
        Technician technician = TechnicianHelper.Create();
        technician.AssignWarrant(warrant);

        warrant.UnassignWarrant();

        warrant.TechnicianId.Should().BeNull();
    }

    [Theory]
    [InlineData(RepairshopClientContext.FrontOffice, false, true)]
    [InlineData(RepairshopClientContext.Workshop, true, false)]
    public async Task Advancing_a_warrant_in_the_wrong_client_context_should_fail(
        string clientContext, 
        bool stepsCanBeTransitionedByFrontOffice,
        bool stepsCanBeTransitionedByWorkshop)
    {
        // Arrange
        IEnumerable<WarrantStep> steps = 
            await WarrantStepHelper
                .CreateStepSequence(
                    numberOfSteps: 3, 
                    canBeTransitionedByFrontOffice: stepsCanBeTransitionedByFrontOffice,
                    canBeTransitionedByWorkshop: stepsCanBeTransitionedByWorkshop);

        Warrant warrant = await WarrantHelper.Create(steps: steps);

        // Act
        Action act = 
            () => warrant.AdvanceToStep(
                steps.Select(x => x.Id).ElementAt(1), 
                clientContext, 
                GetUtcNow);

        // Assert
        act.Should().Throw<DomainInvalidOperationException>();
    }

    [Theory]
    [InlineData(RepairshopClientContext.FrontOffice, false, true)]
    [InlineData(RepairshopClientContext.Workshop, true, false)]
    public async Task Rolling_back_a_warrant_in_the_wrong_client_context_should_fail(
        string clientContext,
        bool stepsCanBeTransitionedByFrontOffice,
        bool stepsCanBeTransitionedByWorkshop)
    {
        // Arrange
        IEnumerable<WarrantStep> steps =
            await WarrantStepHelper
                .CreateStepSequence(
                    numberOfSteps: 3, 
                    canBeTransitionedByFrontOffice: stepsCanBeTransitionedByFrontOffice,
                    canBeTransitionedByWorkshop: stepsCanBeTransitionedByWorkshop);

        Warrant warrant = await WarrantHelper.Create(steps: steps);

        // Use authorized client context for arranging
        warrant.AdvanceToStep(
            steps.Select(x => x.Id).ElementAt(1),
            clientContext == RepairshopClientContext.FrontOffice 
                ? RepairshopClientContext.Workshop 
                : RepairshopClientContext.FrontOffice, 
            GetUtcNow);

        // Act
        Action act =
            () => warrant.RollbackToStep(
                steps.Select(x => x.Id).First(),
                clientContext, 
                GetUtcNow);

        act.Should().Throw<DomainInvalidOperationException>();
    }

    [Fact]
    public async void Creating_a_warrant_should_create_a_log_entry()
    {
        DateTimeOffset logEntryTime = 
            new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.FromHours(0));

        Warrant warrant = await WarrantHelper.Create(getUtcNow: () => logEntryTime);

        warrant.LogEntries.Should().HaveCount(1);
        warrant.LogEntries.First().Should().Match<WarrantLogEntry>(x =>
            x.NewState == warrant.Steps.First().Procedure.Name
                && x.EventTime == logEntryTime
                && x.WarrantNumber == warrant.Number);
    }

    [Fact]
    public async void Updating_a_warrants_status_should_create_a_valid_log_entry()
    {
        // Arrange
        Warrant warrant = await WarrantHelper.Create();
        Technician technician = TechnicianHelper.Create();
        technician.AssignWarrant(warrant);

        DateTimeOffset updatedLogEntryTime =
            new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.FromHours(0));

        // Act
        warrant.AdvanceToStep(
            warrant.Steps.Skip(1).First().Id,
            RepairshopClientContext.FrontOffice,
            () => updatedLogEntryTime);

        // Assert
        warrant.LogEntries.Should().ContainSingle(x => x.EventTime == updatedLogEntryTime
                && x.PreviousState == warrant.Steps.First().Procedure.Name
                && x.NewState == warrant.Steps.Skip(1).First().Procedure.Name
                && x.WarrantNumber == warrant.Number
                && x.TechnicianName == technician.Name);
    }

    [Fact]
    public async void Advancing_a_warrant_should_create_a_log_entry()
    {
        Warrant warrant = await WarrantHelper.Create();

        warrant.AdvanceToStep(
            warrant.Steps.Skip(1).First().Id, 
            RepairshopClientContext.FrontOffice, 
            GetUtcNow);

        warrant.LogEntries.Should().HaveCount(2);
    }

    [Fact]
    public async void Rolling_back_a_warrant_should_create_a_log_entry()
    {
        // Arrange
        Warrant warrant = await WarrantHelper.Create();
    
        warrant.AdvanceToStep(
            warrant.Steps.Skip(1).First().Id,
            RepairshopClientContext.FrontOffice,
            GetUtcNow);

        // Act
        warrant.RollbackToStep(
            warrant.Steps.First().Id,
            RepairshopClientContext.FrontOffice,
            GetUtcNow);
    
        // Assert
        warrant.LogEntries.Should().HaveCount(3);
    }

    private DateTimeOffset GetUtcNow() => DateTimeOffset.UtcNow;
}
