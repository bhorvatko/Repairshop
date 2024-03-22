﻿using FluentAssertions;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;

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

        warrant.AdvanceToStep(steps.Skip(1).First().Id);

        warrant.CurrentStep.Should().Be(steps.Skip(1).First());
    }

    [Fact]
    public async Task Advancing_a_warrant_to_a_step_which_is_not_next_in_the_sequence_should_fail()
    {
        IEnumerable<WarrantStep> steps = await WarrantStepHelper.CreateStepSequence(3);
        Warrant warrant = await WarrantHelper.Create(steps: steps);
        warrant.SetInitialStep();

        Action act = () => warrant.AdvanceToStep(steps.Last().Id);

        act.Should().Throw<DomainArgumentException>();
    }
}