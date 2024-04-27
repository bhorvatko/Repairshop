using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.ClientContext;

namespace Repairshop.Client.Infrastructure.ClientContext;

internal static class Setup
{
    public static IServiceCollection AddClientContext(
        this IServiceCollection services,
        string clientContext) =>
        services
            .AddTransient<IClientContextProvider>(_ => new ClientContextProvider(clientContext));

}
