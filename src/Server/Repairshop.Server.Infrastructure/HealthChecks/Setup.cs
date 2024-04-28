using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Shared.Common.HealthChecks;

namespace Repairshop.Server.Infrastructure.HealthChecks;

internal static class Setup
{
    public static IServiceCollection AddApiHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks();

        return services;
    }

    public static WebApplication UseApiHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks($"/{HealthCheckConstants.HeathlCheckEndpoint}");

        return app;
    }
}
