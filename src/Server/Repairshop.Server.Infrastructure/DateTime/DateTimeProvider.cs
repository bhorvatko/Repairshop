using Repairshop.Server.Common.DateTime;

namespace Repairshop.Server.Infrastructure.DateTime;

internal class DateTimeProvider
    : IDateTimeProvider
{
    public DateTimeOffset GetUtcNow() =>
        DateTimeOffset.UtcNow;
}
