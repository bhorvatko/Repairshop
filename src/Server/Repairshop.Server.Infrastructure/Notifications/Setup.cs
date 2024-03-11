using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Server.Common.Notifications;

namespace Repairshop.Server.Infrastructure.Notifications;
internal static class Setup
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services
            .AddSignalR();

        services.AddTransient<INotificationDispatcher, NotificationDispatcher>();

        return services;
    }

    public static WebApplication UseNotifications(this WebApplication app)
    {
        app.MapHub<NotificationsHub>("/Notifications");

        return app;
    }
}
