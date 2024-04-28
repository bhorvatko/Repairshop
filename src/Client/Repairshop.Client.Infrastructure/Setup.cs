using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Infrastructure.ApiClient;
using Repairshop.Client.Infrastructure.ClientContext;
using Repairshop.Client.Infrastructure.Forms;
using Repairshop.Client.Infrastructure.HealthChecks;
using Repairshop.Client.Infrastructure.LoadingIndicator;
using Repairshop.Client.Infrastructure.MessageDialog;
using Repairshop.Client.Infrastructure.Navigation;
using Repairshop.Client.Infrastructure.Notifications;
using Repairshop.Client.Infrastructure.Services;
using Repairshop.Client.Infrastructure.UserNotifications;
using Repairshop.Client.Infrastructure.UserSettings;

namespace Repairshop.Client.Infrastructure;

public static class Setup
{
    public static IServiceCollection AddInfrastructure<TMainViewModel, TMainView>(
        this IServiceCollection services,
        IConfiguration config,
        string clientContext)
        where TMainViewModel : IMainViewModel
        where TMainView : MainView =>
        services
            .AddLoadingIndicator<TMainViewModel>()
            .AddNavigation<TMainView>()
            .AddApiClient(config)
            .AddApplicationServices()
            .AddMessageDialog()
            .AddClientContext(clientContext)
            .AddUserSettings()
            .AddNotifications()
            .AddForms()
            .AddUserNotifications()
            .AddHealthChecks();
}
