using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;
using System.Reflection;

namespace Repairshop.Client.Infrastructure.Navigation;
internal static class Setup
{
    public static IServiceCollection AddNavigation<TMainViewModel>(this IServiceCollection services)
        where TMainViewModel : IMainViewModel
    {
        services
            .AddTransient<IDialogService, DialogService>(sp => new DialogService(sp))
            .AddSingleton<INavigationService, NavigationService>(sp =>
                new NavigationService(
                    viewModelType => (IViewModel)sp.GetRequiredService(viewModelType),
                    viewModel => sp.GetRequiredService<TMainViewModel>().CurrentViewModel = viewModel));

        services.Scan(scan =>
            scan
                .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
                .AddClasses(classes => classes.AssignableTo<IDialogViewModel>())
                .AddClasses(classes => classes.AssignableTo<IDialogContent>())
                .AsSelf()
                .WithTransientLifetime());

        return services;
    }
}
