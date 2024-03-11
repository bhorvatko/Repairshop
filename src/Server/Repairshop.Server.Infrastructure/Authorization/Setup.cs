using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Server.Infrastructure.Authorization.ApiKey;

namespace Repairshop.Server.Infrastructure.Authorization;
internal static class Setup
{
    public static IServiceCollection AddApiKey(this IServiceCollection services) =>
        services.AddScoped<ApiKeyMiddleware>();

    public static IApplicationBuilder UseApiKey(this IApplicationBuilder app) =>
        app.UseMiddleware<ApiKeyMiddleware>();
}
