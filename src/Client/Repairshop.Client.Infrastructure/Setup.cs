using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Infrastructure.ApiClient;
using Repairshop.Client.Infrastructure.LoadingIndicator;
using Repairshop.Client.Infrastructure.MessageDialog;
using Repairshop.Client.Infrastructure.Navigation;
using Repairshop.Client.Infrastructure.Services;

namespace Repairshop.Client.Infrastructure;

public static class Setup
{
    public static IServiceCollection AddInfrastructure<TMainViewModel>(
        this IServiceCollection services,
        IConfiguration config)
        where TMainViewModel : IMainViewModel =>
        services
            .AddLoadingIndicator<TMainViewModel>()
            .AddNavigation<TMainViewModel>()
            .AddApiClient(config)
            .AddApplicationServices()
            .AddMessageDialog();
}
