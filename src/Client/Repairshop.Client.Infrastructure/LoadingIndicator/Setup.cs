using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;

namespace Repairshop.Client.Infrastructure.LoadingIndicator;
internal static class Setup
{
    public static IServiceCollection AddLoadingIndicator<TMainViewModel>(this IServiceCollection services)
        where TMainViewModel : IMainViewModel =>
        services
            .AddSingleton<ILoadingIndicatorService, LoadingIndicatorService>(sp => 
                new LoadingIndicatorService(
                    sp.GetRequiredService<TMainViewModel>(),
                    sp.GetRequiredService<IMessageDialogService>()));
}
