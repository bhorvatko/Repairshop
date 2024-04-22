using FluentAssertions;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Server.Features.WarrantManagement.WarrantTemplates;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;
using static Repairshop.Server.Features.WarrantManagement.WarrantTemplates.WarrantTemplateStep;

namespace Repairshop.Server.UnitTests.Features.WarrantManagement.WarrantTemplates;

public class WarrantTemplateStepTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(100)]
    public async Task Creating_a_warrant_template_step_sequence_with_one_or_more_steps_should_succeed(int numberOfSteps)
    {
        // Arrange
        IEnumerable<Procedure> procedures = ProcedureHelper.Create(numberOfSteps);

        IEnumerable<CreateWarrantStepArgs> stepArgs =
            procedures.Select(x => new CreateWarrantStepArgs(x.Id, true, true));

        GetProceduresByIdDelegate getProceduresById =
            ids => Task.FromResult(procedures.Where(p => ids.Contains(p.Id)));

        // Act
        IEnumerable<WarrantTemplateStep> steps =
            await WarrantTemplateStep.CreateStepSequence(stepArgs, getProceduresById);

        // Assert
        steps.Count().Should().Be(numberOfSteps);
    }

    [Fact]
    public async Task Creating_a_warrant_template_step_sequence_without_any_steps_should_fail()
    {
        IEnumerable<CreateWarrantStepArgs> stepArgs = Enumerable.Empty<CreateWarrantStepArgs>();

        Func<Task> act = async () => await WarrantTemplateStep.CreateStepSequence(stepArgs, null!);

        await act
            .Should()
            .ThrowAsync<DomainArgumentException>()
            .Where(ex => ex.InvalidArgument == stepArgs);
    }

    [Fact]
    public async Task Creating_a_warrant_template_step_sequence_with_repeating_procedures_should_fail()
    {
        Guid procedureId = Guid.NewGuid();

        IEnumerable<CreateWarrantStepArgs> stepArgs = new[]
        {
            new CreateWarrantStepArgs(procedureId, true, true),
            new CreateWarrantStepArgs(procedureId, true, true),
        };

        Func<Task> act = async () => await WarrantTemplateStep.CreateStepSequence(stepArgs, null!);

        await act
            .Should()
            .ThrowAsync<DomainArgumentException>()
            .Where(ex => ex.InvalidArgument == stepArgs);
    }

    [Fact]
    public async Task Creating_a_warrant_template_step_sequence_with_a_non_existent_procedure_should_fail()
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
        Func<Task> act = async () => await WarrantTemplateStep.CreateStepSequence(stepArgs, getProceduresById);

        // Assert
        await act
            .Should()
            .ThrowAsync<EntityNotFoundException<Procedure, Guid>>();
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
            async () => await WarrantTemplateStep.CreateStepSequence(stepArgs, getProceduresById);

        // Assert
        await act
            .Should()
            .ThrowAsync<DomainArgumentException>()
            .Where(x => x.InvalidArgument == stepArgs);
    }
}
