using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Repairshop.Server.Infrastructure.Authorization.ApiKey;
internal class ApiKeyMiddleware
    : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context, 
        RequestDelegate next)
    {
        if (!context.Request.Headers.TryGetValue("X-API-KEY", out var apiKey))
        {
            ProblemDetails problemDetails = GetProblemDetails("API Key was not provided");

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await context.Response.WriteAsJsonAsync(problemDetails);

            return;
        }

        IEnumerable<string> validKeys = context
            .RequestServices
            .GetRequiredService<IConfiguration>()
            .GetSection(ApiKeyOptions.SectionName)
            .Get<ApiKeyOptions>()!
            .ValidKeys;

        if (!validKeys.Contains(apiKey.ToString()))
        {
            ProblemDetails problemDetails = GetProblemDetails("Unauthorized client");

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await context.Response.WriteAsJsonAsync(problemDetails);

            return;
        }

        await next(context);
    }

    private static ProblemDetails GetProblemDetails(string detail) =>
        new ProblemDetails()
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Detail = detail
        };
}
