using FluentAssertions;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Tests.Shared.Features.WarrantManagement;

namespace Repairshop.Server.UnitTests.Features.WarrantManagement.Procedures;

public class ProcedureTests
{
    [Fact]
    public void Creating_a_valid_procedure()
    {
        Procedure procedure = Procedure.Create(
            "test", 
            ColorCode.FromHexCode("FFFFFF"),
            ProcedurePriority.FromFloating(1));

        procedure.Color.Value.Should().Be("FFFFFF");
        procedure.Name.Should().Be("test");
    }

    [Fact]
    public void Updating_a_procedure()
    {
        Procedure procedure = Procedure.Create(
            "old", 
            ColorCode.FromHexCode("FFFFFF"),
            ProcedurePriority.FromFloating(1));

        procedure.Update("new", ColorCode.FromHexCode("000000"));

        procedure.Name.Should().Be("new");
        procedure.Color.Value.Should().Be("000000");
    }

    [Fact]
    public void Setting_the_procedures_priority()
    {
        Procedure procedure = ProcedureHelper.Create();

        procedure.SetPriority(ProcedurePriority.FromFloating(2));

        procedure.Priority.Value.Should().Be(2);
    }
}
