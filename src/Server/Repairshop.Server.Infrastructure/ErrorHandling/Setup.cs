using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Repairshop.Server.Infrastructure.ErrorHandling;
internal static class Setup
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services) =>
        services
            .AddExceptionHandler<DomainArgumentExceptionHandler>()
            .AddExceptionHandler<EntityNotFoundExceptionHandler>()
            .AddExceptionHandler<DomainInvalidOperationExceptionHandler>()
            .AddExceptionHandler<BadRequestExceptionHandler>()
            .AddProblemDetails();

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app) =>
        app.UseExceptionHandler();
}
