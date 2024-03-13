using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Features.WarrantManagement;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Infrastructure;
using Repairshop.Client.Infrastructure.ApiClient;
using System.Windows;

namespace Repairshop.Client.FrontDesk;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IServiceProvider _serviceProvider;

    public App()
    {
        AppDomain.CurrentDomain.UnhandledException += HandleUnhandledExceptions;

        ServiceCollection services = new ServiceCollection();

        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        services
            .Configure<ApiOptions>(config.GetSection(ApiOptions.SectionName));

        services
            .AddSingleton<MainViewModel>()
            .AddSingleton<MainWindow>()
            .AddInfrastructure<MainViewModel>(config)
            .AddWarrantManagement();

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
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
}

