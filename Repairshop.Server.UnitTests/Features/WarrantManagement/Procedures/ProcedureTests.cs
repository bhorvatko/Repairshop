using FluentAssertions;
using Repairshop.Server.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.UnitTests.Features.WarrantManagement.Procedures;

public class ProcedureTests
{
    [Fact]
    public void Creating_a_valid_procedure()
    {
        Procedure procedure = Procedure.Create(
            "test", 
            ColorCode.FromHexCode("FFFFFF"));

        procedure.Color.Value.Should().Be("FFFFFF");
        procedure.Name.Should().Be("test");
    }

    [Fact]
    public void Updating_a_procedure()
    {
        Procedure procedure = Procedure.Create(
            "old", 
            ColorCode.FromHexCode("FFFFFF"));

        procedure.Update("new", ColorCode.FromHexCode("000000"));

        procedure.Name.Should().Be("new");
        procedure.Color.Value.Should().Be("000000");
    }
}
