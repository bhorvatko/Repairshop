using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;

namespace Repairshop.Client.Infrastructure.UserSettings;

internal static class Setup
{
    public static IServiceCollection AddUserSettings(
        this IServiceCollection services)
    {
        services.AddSingleton(
            typeof(IUserSettingsProvider<>),
            typeof(UserSettingsProvider<>));

        return services;
    }
}
