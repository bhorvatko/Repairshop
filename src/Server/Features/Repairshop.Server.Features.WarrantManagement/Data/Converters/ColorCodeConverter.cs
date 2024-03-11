using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repairshop.Server.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Data.Converters;
internal class ColorCodeConverter
    : ValueConverter<ColorCode, string>
{
    public ColorCodeConverter() 
        : base(
             colorCode => colorCode.Value,
             s => ColorCode.FromHexCode(s))
    {
    }
}
