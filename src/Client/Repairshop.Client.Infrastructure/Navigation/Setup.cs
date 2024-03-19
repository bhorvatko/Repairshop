using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using System.Reflection;

namespace Repairshop.Client.Infrastructure.Navigation;
internal static class Setup
{
    public static IServiceCollection AddNavigation<TMainView>(this IServiceCollection services)
        where TMainView : MainView
    {
        services
            .AddSingleton<Func<MainView>>(sp => () => sp.GetRequiredService<TMainView>())
            .AddTransient<IDialogService, DialogService>(sp => new DialogService(sp))
            .AddSingleton<INavigationService, NavigationService>();

        services.Scan(scan =>
            scan
                .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
                .AddClasses(classes => classes.AssignableTo<IDialogViewModel>())
                .AddClasses(classes => classes.AssignableTo<IDialogContent>())
                .AddClasses(classes => classes.AssignableTo(typeof(ViewBase<>)))
                .AddClasses(classes => classes.AssignableTo<IViewModel>())
                .AsSelf()
                .WithTransientLifetime());

        return services;
    }
}
