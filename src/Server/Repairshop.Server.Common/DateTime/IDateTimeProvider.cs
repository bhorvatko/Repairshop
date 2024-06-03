namespace Repairshop.Server.Common.DateTime;

public interface IDateTimeProvider
{
    DateTimeOffset GetUtcNow();
}
