using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.HealthChecks;

namespace Repairshop.Client.Infrastructure.HealthChecks;

internal static class Setup
{
    public static IServiceCollection AddHealthChecks(this IServiceCollection services)
    {
        services
            .AddSingleton<IServerAvailabilityProvider, ServerAvailibilityProvider>();

        return services;
    }
}
