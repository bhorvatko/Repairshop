using FluentAssertions;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using static Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStep;

namespace Repairshop.Server.UnitTests.Features.WarrantManagement.Warrants;

public class WarrantStepTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(100)]
    public async Task Creating_a_warrant_step_sequence_with_one_or_more_steps_should_succeed(int numberOfSteps)
    {
        // Arrange
        IEnumerable<Procedure> procedures = ProcedureHelper.Create(numberOfSteps);

        IEnumerable<CreateWarrantStepArgs> stepArgs = 
            procedures.Select(x => new CreateWarrantStepArgs(x.Id, true, true));

        GetProceduresByIdDelegate getProceduresById =
            ids => Task.FromResult(procedures.Where(p => ids.Contains(p.Id)));

        // Act
        IEnumerable<WarrantStep> steps = 
            await WarrantStep.CreateStepSequence(stepArgs, getProceduresById);

        // Assert
        steps.Count().Should().Be(numberOfSteps);
    }

    [Fact]
    public async Task Creating_a_step_sequence_without_any_steps_should_fail()
    {
        IEnumerable<CreateWarrantStepArgs> stepArgs = Enumerable.Empty<CreateWarrantStepArgs>();

        Func<Task> act = async () => await WarrantStep.CreateStepSequence(stepArgs, null!);

        await act
            .Should()
            .ThrowAsync<DomainArgumentException>()
            .Where(ex => ex.InvalidArgument == stepArgs);
    }

    [Fact]
    public async Task Creating_a_step_sequence_with_repeating_procedures_should_fail()
    {
        Guid procedureId = Guid.NewGuid();

        IEnumerable<CreateWarrantStepArgs> stepArgs = new[]
        {
            new CreateWarrantStepArgs(procedureId, true, true),
            new CreateWarrantStepArgs(procedureId, true, true),
        };

        Func<Task> act = async () => await WarrantStep.CreateStepSequence(stepArgs, null!);

        await act
            .Should()
            .ThrowAsync<DomainArgumentException>()
            .Where(ex => ex.InvalidArgument == stepArgs);
    }

    [Fact]
    public async Task Creating_a_step_sequence_with_a_non_existent_procedure_should_fail()
    {
        // Arrange
        IEnumerable<Procedure> procedures = ProcedureHelper.Create(3);

        IEnumerable<CreateWarrantStepArgs> stepArgs = new[]
        {
            new CreateWarrantStepArgs(Guid.NewGuid(), true, true),
        };

        GetProceduresByIdDelegate getProceduresById =
            ids => Task.FromResult(procedures.Where(p => ids.Contains(p.Id)));

        // Act
        Func<Task> act = async () => await WarrantStep.CreateStepSequence(stepArgs, getProceduresById);

        // Assert
        await act
            .Should()
            .ThrowAsync<EntityNotFoundException<Procedure, Guid>>();
    }

    [Fact]
    public async Task Creating_a_step_sequence_should_create_a_sequence_in_the_correct_order()
    {
        // Arrange
        IEnumerable<Procedure> procedures = ProcedureHelper.Create(3);

        IEnumerable<CreateWarrantStepArgs> stepArgs =
            procedures.Select(x => new CreateWarrantStepArgs(x.Id, true, true));

        GetProceduresByIdDelegate getProceduresById =
            ids => Task.FromResult(procedures.Where(p => ids.Contains(p.Id)));

        // Act
        IEnumerable<WarrantStep> steps =
            await WarrantStep.CreateStepSequence(stepArgs, getProceduresById);

        // Assert
        bool sequenceInOrderFromStartToEnd =
            steps
                .Single(x => x.ProcedureId == procedures.First().Id)
                .NextTransition?
                .NextStep?
                .NextTransition?
                .NextStep != null;

        bool sequenceInOrderFromEndToStart =
            steps
                .Single(x => x.ProcedureId == procedures.Last().Id)
                .PreviousTransition?
                .PreviousStep?
                .PreviousTransition?
                .PreviousStep != null;

        sequenceInOrderFromStartToEnd.Should().BeTrue();
        sequenceInOrderFromEndToStart.Should().BeTrue();
    }

    [Fact]
    public async Task First_step_in_step_sequence_should_not_have_a_previous_transition()
    {
        // Arrange
        IEnumerable<Procedure> procedures = ProcedureHelper.Create(3);

        IEnumerable<CreateWarrantStepArgs> stepArgs =
            procedures.Select(x => new CreateWarrantStepArgs(x.Id, true, true));

        GetProceduresByIdDelegate getProceduresById =
            ids => Task.FromResult(procedures.Where(p => ids.Contains(p.Id)));

        // Act
        IEnumerable<WarrantStep> steps =
            await WarrantStep.CreateStepSequence(stepArgs, getProceduresById);

        // Assert
        steps.First(x => x.ProcedureId == procedures.First().Id)
            .PreviousTransition
            .Should()
            .BeNull();
    }

    [Fact]
    public async Task Last_step_in_step_sequence_should_not_have_a_next_transition()
    {
        // Arrange
        IEnumerable<Procedure> procedures = ProcedureHelper.Create(3);

        IEnumerable<CreateWarrantStepArgs> stepArgs =
            procedures.Select(x => new CreateWarrantStepArgs(x.Id, true, true));

        GetProceduresByIdDelegate getProceduresById =
            ids => Task.FromResult(procedures.Where(p => ids.Contains(p.Id)));

        // Act
        IEnumerable<WarrantStep> steps =
            await WarrantStep.CreateStepSequence(stepArgs, getProceduresById);

        // Assert
        steps.First(x => x.ProcedureId == procedures.Last().Id)
            .NextTransition
            .Should()
            .BeNull();
    }

    [Fact]
    public async Task Creating_a_procedure_creates_correct_transition_permissions()
    {
        // Arrange
        IEnumerable<Procedure> procedures = ProcedureHelper.Create(2);

        IEnumerable<CreateWarrantStepArgs> stepArgs = new[]
        {
            new CreateWarrantStepArgs(procedures.First().Id, false, false),
            new CreateWarrantStepArgs(procedures.Last().Id, true, true),
        };

        GetProceduresByIdDelegate getProceduresById =
            ids => Task.FromResult(procedures.Where(p => ids.Contains(p.Id)));

        // Act
        IEnumerable<WarrantStep> steps =
            await WarrantStep.CreateStepSequence(stepArgs, getProceduresById);

        // Assert
        WarrantStepTransition transitionBetweenSteps =
            steps
                .Single(x => x.ProcedureId == procedures.First().Id)
                .NextTransition!;

        transitionBetweenSteps.CanBePerformedByFrontOffice.Should().BeTrue();
        transitionBetweenSteps.CanBePerformedByWorkshop.Should().BeTrue();
    }

    [Fact]
    public async Task Creating_a_step_which_is_not_advancable_should_fail()
    {
        // Arrange
        IEnumerable<Procedure> procedures = ProcedureHelper.Create(2);

        IEnumerable<CreateWarrantStepArgs> stepArgs = new[]
        {
            new CreateWarrantStepArgs(procedures.First().Id, false, false),
            new CreateWarrantStepArgs(procedures.Last().Id, false, false)
        };

        GetProceduresByIdDelegate getProceduresById =
            ids => Task.FromResult(procedures.Where(p => ids.Contains(p.Id)));

        // Act
        Func<Task> act = 
            async () => await WarrantStep.CreateStepSequence(stepArgs, getProceduresById);

        // Assert
        await act
            .Should()
            .ThrowAsync<DomainArgumentException>()
            .Where(x => x.InvalidArgument == stepArgs);
    }
}
