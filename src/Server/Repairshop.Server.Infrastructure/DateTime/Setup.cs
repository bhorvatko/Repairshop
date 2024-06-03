using Microsoft.Extensions.DependencyInjection;
using Repairshop.Server.Common.DateTime;

namespace Repairshop.Server.Infrastructure.DateTime;

internal static class Setup
{
    public static IServiceCollection AddDateTime(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
