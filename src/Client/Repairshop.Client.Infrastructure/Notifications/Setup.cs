using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Infrastructure.ApiClient;
using Repairshop.Shared.Common.Notifications;

namespace Repairshop.Client.Infrastructure.Notifications;

internal static class Setup
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddSingleton<IWarrantNotificationService, WarrantNotificationService>();
        services.AddSingleton<HubConnection>(sp =>
        {
            ApiOptions apiOptions = 
                sp.GetRequiredService<IOptions<ApiOptions>>().Value;

            return new HubConnectionBuilder()
                .WithUrl(
                    $"{apiOptions.BaseAddress}/{NotificationConstants.NotificationsEndpoint}",
                    options =>
                    {
                        options.Headers.Add("X-API-KEY", apiOptions.ApiKey);
                    })
                .WithAutomaticReconnect()
                .Build();
        });

        return services;
    }
}
