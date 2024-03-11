using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Server.Common.Exceptions;

namespace Repairshop.Server.Infrastructure.ErrorHandling;
internal static class Setup
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services) =>
        services
            .AddExceptionHandler<DomainArgumentExceptionHandler>()
            .AddExceptionHandler<EntityNotFoundExceptionHandler>()
            .AddExceptionHandler<DomainInvalidOperationExceptionHandler>()
            .AddProblemDetails();

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app) =>
        app.UseExceptionHandler();
}
