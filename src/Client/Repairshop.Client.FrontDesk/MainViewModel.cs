using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Extensions;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.HealthChecks;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Client.Features.WarrantManagement.Technicians;
using Repairshop.Client.Features.WarrantManagement.Warrants;
using Repairshop.Client.Features.WarrantManagement.WarrantTemplates;
using Repairshop.Client.Infrastructure.Bootstrapping;
using Repairshop.Client.Infrastructure.UserNotifications;
using System.Windows;

namespace Repairshop.Client.FrontDesk;

public partial class MainViewModel
    : MainViewModelBase, IDisposable
{
    private const string _loadingText = "Učitavanje...";
    private const string _serverDownText = "Server je trenutačno nedostupan...";

    private readonly INavigationService _navigationService;
    private readonly IFormService _formService;

    private IDisposable _serverAvailabilitySubscription;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LoadingIndicatorVisibility))]
    [NotifyPropertyChangedFor(nameof(LoadingIndicatorText))]
    private bool _loading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LoadingIndicatorVisibility))]
    [NotifyPropertyChangedFor(nameof(LoadingIndicatorText))]
    private bool _serverDown;

    public MainViewModel(
        INavigationService navigationService,
        IFormService formService,
        ToastNotificationContainerViewModel toastNotificationContainerViewModel,
        IServerAvailabilityProvider serverAvailabilityProvider)
    {
        _navigationService = navigationService;
        _formService = formService;
       
        ToastNotificationContainerViewModel = toastNotificationContainerViewModel;

        _serverAvailabilitySubscription = 
            serverAvailabilityProvider.SubscribeToServerAvailability(OnServerAvailabilityChanged);
    }

    public ToastNotificationContainerViewModel ToastNotificationContainerViewModel { get; private set; }

    public string LoadingIndicatorText =>
        Loading ? _loadingText : _serverDownText;

    public override Visibility LoadingIndicatorVisibility => (Loading || ServerDown).ToVisibility();

    [RelayCommand]
    public void NavigateToDashboard()
    {
        _navigationService.NavigateToView<DashboardView>();
    }

    [RelayCommand]
    public void NavigateToProceduresView()
    {
        _navigationService.NavigateToView<ProceduresView>();
    }

    [RelayCommand]
    public void NavigateToCreateWarrantView()
    {
        _formService.ShowForm<CreateWarrantView>();
    }

    [RelayCommand]
    public void NavigateToAddTechnicianView()
    {
        _navigationService.NavigateToView<AddTechnicianView>();
    }

    [RelayCommand]
    public void NavigateToCreateWarrantTemplateView()
    {
        _navigationService.NavigateToView<CreateWarrantTemplateView>();
    }

    [RelayCommand]
    public void NavigateToTechnicianMaintenanceView()
    {
        _navigationService.NavigateToView<TechnicianMaintenanceView>();
    }

    public void Dispose()
    {
        _serverAvailabilitySubscription.Dispose();
    }

    public override void ShowLoadingIndicator() => Loading = true;

    public override void HideLoadingIndicator() => Loading = false;

    private void OnServerAvailabilityChanged(bool serverAvailable) =>
        ServerDown = !serverAvailable;
}
