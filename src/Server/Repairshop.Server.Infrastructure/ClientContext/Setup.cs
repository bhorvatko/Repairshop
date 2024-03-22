using Microsoft.Extensions.DependencyInjection;
using Repairshop.Server.Common.ClientContext;

namespace Repairshop.Server.Infrastructure.ClientContext;

internal static class Setup
{
    public static IServiceCollection AddClientContext(this IServiceCollection services) =>
        services
            .AddTransient<IClientContextProvider, ClientContextProvider>()
            .AddHttpContextAccessor();
}
