using FluentAssertions;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.UnitTests.Features.WarrantManagement.Procedures;

public class ColorCodeTests
{
    [Fact]
    public void Creating_a_valid_color_code()
    {
        ColorCode colorCode = ColorCode.FromHexCode("FFFFFF");

        colorCode.Value.Should().Be("FFFFFF");
    }

    [Fact]
    public void Colorcode_in_nonhex_format_is_invalid()
    {
        string value = "GFFFFF";

        Action action = () => ColorCode.FromHexCode(value);

        action
            .Should()
            .Throw<DomainArgumentException>()
            .Where(ex => ex.InvalidArgument == (object)value);
    }

    [Fact]
    public void Colorcode_out_of_range_is_invalid()
    {
        string value = "1000000";

        Action action = () => ColorCode.FromHexCode(value);

        action
            .Should()
            .Throw<DomainArgumentException>()
            .Where(ex => ex.InvalidArgument == (object)value);
    }
}
