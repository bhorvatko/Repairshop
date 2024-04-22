﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Server.Common.Notifications;
using Repairshop.Shared.Common.Notifications;

namespace Repairshop.Server.Infrastructure.Notifications;
internal static class Setup
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services
            .AddSignalR();

        services
            .AddTransient<INotificationDispatcher, NotificationDispatcher>()
            .AddTransient<HubConnectionIdProvider>();

        return services;
    }

    public static WebApplication UseNotifications(this WebApplication app)
    {
        app.MapHub<NotificationsHub>($"/{NotificationConstants.NotificationsEndpoint}");

        return app;
    }
}
