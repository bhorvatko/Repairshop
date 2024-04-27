using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.UserNotifications;

namespace Repairshop.Client.Infrastructure.UserNotifications;

internal static class Setup
{
    public static IServiceCollection AddUserNotifications(this IServiceCollection services)
    {
        services
            .AddTransient<IToastNotificationService, ToastNotificationService>()
            .AddSingleton<ToastNotificationContainerViewModel>();

        return services;
    }
}
