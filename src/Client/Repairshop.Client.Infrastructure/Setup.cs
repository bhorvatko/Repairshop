﻿using Microsoft.Extensions.Configuration;
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
    public static IServiceCollection AddInfrastructure<TMainViewModel, TMainView>(
        this IServiceCollection services,
        IConfiguration config)
        where TMainViewModel : IMainViewModel
        where TMainView : MainView =>
        services
            .AddLoadingIndicator<TMainViewModel>()
            .AddNavigation<TMainView>()
            .AddApiClient(config)
            .AddApplicationServices()
            .AddMessageDialog();
}
