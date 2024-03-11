using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Repairshop.Server.Infrastructure.Mediator;

internal static class Setup
{
    public static IServiceCollection AddMediator(this IServiceCollection services) =>
        services
            .AddMediatR(config =>
            {
                //config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
                config.RegisterServicesFromAssembly(Assembly.Load("Repairshop.Server.Features.WarrantManagement"));

            });
}
