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
        IEnumerable<WarrantStep> steps = await WarrantStepHelper.CreateStepSequence(3);

        Warrant warrant = Warrant.Create(
            title,
            deadline,
            isUrgent,
            steps);

        warrant.Title.Should().Be(title);
        warrant.Deadline.Should().Be(deadline);
        warrant.IsUrgent.Should().Be(isUrgent);
        warrant.Steps.Count().Should().Be(steps.Count());
    }

    [Fact]
    public async Task Advancing_a_warrant_to_the_next_step()
    {
        IEnumerable<WarrantStep> steps = await WarrantStepHelper.CreateStepSequence(3);
        Warrant warrant = await WarrantHelper.Create(steps: steps);
        warrant.SetInitialStep();

        warrant.AdvanceToStep(steps.Skip(1).First().Id, RepairshopClientContext.FrontOffice);

        warrant.CurrentStep.Should().Be(steps.Skip(1).First());
    }

    [Fact]
    public async Task Advancing_a_warrant_to_a_step_which_is_not_next_in_the_sequence_should_fail()
    {
        IEnumerable<WarrantStep> steps = await WarrantStepHelper.CreateStepSequence(3);
        Warrant warrant = await WarrantHelper.Create(steps: steps);
        warrant.SetInitialStep();

        Action act = 
            () => warrant.AdvanceToStep(steps.Last().Id, RepairshopClientContext.FrontOffice);

        act.Should().Throw<DomainArgumentException>();
    }

    [Fact]
    public async Task Rollback_a_warrant_to_the_previous_step()
    {
        IEnumerable<WarrantStep> steps = await WarrantStepHelper.CreateStepSequence(3);
        Warrant warrant = await WarrantHelper.Create(steps: steps);
        warrant.SetInitialStep();
        warrant.AdvanceToStep(steps.Select(x => x.Id).ElementAt(1), RepairshopClientContext.FrontOffice);

        warrant.RollbackToStep(steps.First().Id, RepairshopClientContext.FrontOffice);

        warrant.CurrentStep.Should().Be(steps.First());
    }

    [Fact]
    public async Task Rollback_a_warrant_to_a_step_which_is_not_previous_in_the_sequence_should_fail()
    {
        IEnumerable<WarrantStep> steps = await WarrantStepHelper.CreateStepSequence(3);
        Warrant warrant = await WarrantHelper.Create(steps: steps);
        warrant.SetInitialStep();
        warrant.AdvanceToStep(steps.Select(x => x.Id).ElementAt(1), RepairshopClientContext.FrontOffice);

        Action act = () => warrant.RollbackToStep(steps.Last().Id, RepairshopClientContext.FrontOffice);

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
        IEnumerable<WarrantStep> updatedSteps = warrant.Steps.Take(2);

        // Act
        warrant.Update(
            updatedTitle,
            updatedDeadline,
            updatedIsUrgent,
            updatedSteps);

        // Assert
        warrant.Should().Match<Warrant>(x =>
            x.Title == updatedTitle
                && x.Deadline == updatedDeadline
                && x.IsUrgent == updatedIsUrgent
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
        warrant.SetInitialStep();

        // Act
        Action act = 
            () => warrant.AdvanceToStep(
                steps.Select(x => x.Id).ElementAt(1), 
                clientContext);

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
        warrant.SetInitialStep();

        // Use authorized client context for arranging
        warrant.AdvanceToStep(
            steps.Select(x => x.Id).ElementAt(1),
            clientContext == RepairshopClientContext.FrontOffice 
                ? RepairshopClientContext.Workshop 
                : RepairshopClientContext.FrontOffice);

        // Act
        Action act =
            () => warrant.RollbackToStep(
                steps.Select(x => x.Id).First(),
                clientContext);

        act.Should().Throw<DomainInvalidOperationException>();
    }
}
