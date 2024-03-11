using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.ValueObjects;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;

public class ColorCode
    : ValueObject<string>
{
    private ColorCode(string value) 
        : base(value)
    {
    }

    public static ColorCode FromHexCode(string hexCode)
    {
        string normalizedColorCode = hexCode
            .Replace("#", "")
            .ToUpper()
            .Trim();

        if (!long.TryParse(
                normalizedColorCode,
                System.Globalization.NumberStyles.HexNumber,
                null,
                out long colorCode))
        {
            throw new DomainArgumentException(
                hexCode,
                "The color code needs to be a valid hexadecimal code.");
        }

        if (colorCode < 0 || colorCode > 16777215)
        {
            throw new DomainArgumentException(
                hexCode,
                "The color code can have a minimum value of 0 and a maximum value of FFFFFF.");
        }

        return new ColorCode(normalizedColorCode);
    }
}
