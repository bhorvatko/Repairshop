using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement;
using Repairshop.Client.Infrastructure.ApiClient;
using Repairshop.Client.Infrastructure.Navigation;
using System.Windows;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;

namespace Repairshop.Client.Infrastructure.Bootstrapping;

public abstract class AppBase<TMainView, TMainViewModel>
    : Application
    where TMainView : MainView
    where TMainViewModel : class, IMainViewModel
{
    private IServiceProvider _serviceProvider;

    public AppBase(string clientContext)
    {
        AppDomain.CurrentDomain.UnhandledException += HandleUnhandledExceptions;

        ServiceCollection services = new ServiceCollection();

        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
        .Build();

        services
            .Configure<ApiOptions>(config.GetSection(ApiOptions.SectionName));

        services
            .AddSingleton<TMainViewModel>()
            .AddSingleton<TMainView>()
            .AddInfrastructure<TMainViewModel, TMainView>(config, clientContext)
            .AddWarrantManagement();

        _serviceProvider = services.BuildServiceProvider();

        SetupMaterialUi();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        TMainView mainWindow = _serviceProvider.GetRequiredService<TMainView>();
        mainWindow.Show();
    }

    private void HandleUnhandledExceptions(object sender, UnhandledExceptionEventArgs e)
    {
        Exception exception = (Exception)e.ExceptionObject;

        MessageBox.Show(
            messageBoxText: exception.Message,
            caption: "Unexpected error",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Error);
    }

    private void SetupMaterialUi()
    {
        ResourceDictionary resourceDictionary = new()
        {
            Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign3.Defaults.xaml", UriKind.Absolute)
        };

        BundledTheme bundledTheme = new()
        {
            BaseTheme = BaseTheme.Light,
            PrimaryColor = PrimaryColor.LightBlue,
            SecondaryColor = SecondaryColor.Amber
        };

        this.Resources.MergedDictionaries.Add(resourceDictionary);
        this.Resources.MergedDictionaries.Add(bundledTheme);
    }
}
