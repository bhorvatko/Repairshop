using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Infrastructure.Notifications;

internal static class Setup
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddSingleton<IWarrantNotificationService, WarrantNotificationService>();

        return services;
    }
}
