using Microsoft.Extensions.DependencyInjection;

namespace Repairshop.Server.Infrastructure.Persistence;
internal static class Setup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services) =>
        services
            .AddTransient<EventPublishingInterceptor>();
}
