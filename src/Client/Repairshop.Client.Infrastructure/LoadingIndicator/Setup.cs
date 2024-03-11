using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Infrastructure.MessageDialog;

namespace Repairshop.Client.Infrastructure.LoadingIndicator;
internal static class Setup
{
    public static IServiceCollection AddLoadingIndicator<TMainViewModel>(this IServiceCollection services)
        where TMainViewModel : IMainViewModel =>
        services
            .AddSingleton<ILoadingIndicatorService, LoadingIndicatorService>(sp => 
                new LoadingIndicatorService(
                    sp.GetRequiredService<TMainViewModel>(),
                    sp.GetRequiredService<MessageDialogService>()));
}
