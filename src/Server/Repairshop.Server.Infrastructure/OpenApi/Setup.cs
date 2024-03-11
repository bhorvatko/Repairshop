using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Repairshop.Server.Infrastructure.OpenApi;

internal static class Setup
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        return services
            .AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition(
                    "API Key",
                    new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.ApiKey,
                        Name = "X-API-KEY",
                        In = ParameterLocation.Header,
                    });

                OpenApiSecurityScheme key = new()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "API Key"
                    },
                    In = ParameterLocation.Header
                };

                config.AddSecurityRequirement(
                    new OpenApiSecurityRequirement()
                    {
                        { key, new List<string>() }
                    });
            });
    }

    public static WebApplication UseOpenApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app
            .UseSwagger()
            .UseSwaggerUI();
        }

        return app;
    }
}
