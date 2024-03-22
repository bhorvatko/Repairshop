using Microsoft.Extensions.DependencyInjection;

namespace Repairshop.Client.Infrastructure.ClientContext;

internal static class Setup
{
    public static IServiceCollection AddClientContext(
        this IServiceCollection services,
        string clientContext) =>
        services
            .AddTransient<ClientContextProvider>(_ => new ClientContextProvider(clientContext));

}
