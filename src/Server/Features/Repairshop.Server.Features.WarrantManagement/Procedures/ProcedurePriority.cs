using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.ValueObjects;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;

public class ProcedurePriority
    : ValueObject<float>
{
    private ProcedurePriority(float value)
        : base(value)
    {
    }

    public static ProcedurePriority FromFloating(float value)
    {
        float minPriorityValue = float.MinValue / 2;

        if (value < minPriorityValue)
        {
            throw new DomainArgumentException(
                value,
                $"Priority cannot be less than {minPriorityValue}");
        }

        float maxPriorityValue = float.MaxValue / 2;

        if (value > maxPriorityValue)
        {
            throw new DomainArgumentException(
                value,
                $"Priority cannot be greater than {maxPriorityValue}"); 
        }

        return new ProcedurePriority(value);
    }
}
