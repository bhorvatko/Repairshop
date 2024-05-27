using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repairshop.Server.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Data.Converters;

internal class ProcedurePriorityConverter
    : ValueConverter<ProcedurePriority, float>
{
    public ProcedurePriorityConverter()
        : base(
            v => v.Value,
            v => ProcedurePriority.FromFloating(v))
    {
    }
}
