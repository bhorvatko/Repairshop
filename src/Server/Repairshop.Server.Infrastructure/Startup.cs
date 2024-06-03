using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Server.Infrastructure.Authorization;
using Repairshop.Server.Infrastructure.ClientContext;
using Repairshop.Server.Infrastructure.DateTime;
using Repairshop.Server.Infrastructure.ErrorHandling;
using Repairshop.Server.Infrastructure.HealthChecks;
using Repairshop.Server.Infrastructure.Mediator;
using Repairshop.Server.Infrastructure.Notifications;
using Repairshop.Server.Infrastructure.OpenApi;
using Repairshop.Server.Infrastructure.Persistence;
using Serilog;

namespace Repairshop.Server.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure<TProgram>(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddControllers();

        services
            .AddMediator()
            .AddOpenApi()
            .AddErrorHandling()
            .AddApiKey()
            .AddNotifications()
            .AddPersistence()
            .AddClientContext()
            .AddApiHealthChecks()
            .AddDateTime();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app
            .UseSerilogRequestLogging()
            .UseHttpsRedirection()
            .UseErrorHandling();

        app
            .UseApiHealthChecks();

        app
            .MapControllers();

        app
            .UseOpenApi()
            .UseApiKey();

        app
            .UseNotifications();

        return app;
    }
}
