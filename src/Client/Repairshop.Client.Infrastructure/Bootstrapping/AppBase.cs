using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement;
using Repairshop.Client.Infrastructure.ApiClient;
using Repairshop.Client.Infrastructure.Navigation;
using System.Windows;

namespace Repairshop.Client.Infrastructure.Bootstrapping;

public abstract class AppBase<TMainView, TMainViewModel>
    : Application
    where TMainView : MainView
    where TMainViewModel : class, IMainViewModel
{
    private readonly string _clientContext;
    private readonly IHost _host;
    private readonly IServiceProvider _serviceProvider;

    public AppBase(string clientContext)
    {
        AppDomain.CurrentDomain.UnhandledException += HandleUnhandledExceptions;

        _clientContext = clientContext;

        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
            .Build();

        _serviceProvider = _host.Services;

        SetupMaterialUi();
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddOptions();

        services
            .Configure<ApiOptions>(context.Configuration.GetSection(ApiOptions.SectionName));

        services
            .AddSingleton<TMainViewModel>()
            .AddSingleton<TMainView>()
            .AddInfrastructure<TMainViewModel, TMainView>(context.Configuration, _clientContext)
            .AddWarrantManagement();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        TMainView mainWindow = _serviceProvider.GetRequiredService<TMainView>();
        mainWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();

        base.OnExit(e);
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
