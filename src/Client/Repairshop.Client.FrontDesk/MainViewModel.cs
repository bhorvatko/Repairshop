using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Extensions;
using Repairshop.Client.Common.HealthChecks;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Client.Features.WarrantManagement.Technicians;
using Repairshop.Client.Features.WarrantManagement.WarrantLog;
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

    private IDisposable _serverAvailabilitySubscription;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LoadingIndicatorVisibility))]
    [NotifyPropertyChangedFor(nameof(LoadingIndicatorText))]
    private bool _loading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LoadingIndicatorVisibility))]
    [NotifyPropertyChangedFor(nameof(LoadingIndicatorText))]
    private bool _serverDown;

    [ObservableProperty]
    private bool _sideMenuOpen;

    private NavigationItem _selectedNavigationItem;

    public MainViewModel(
        INavigationService navigationService,
        ToastNotificationContainerViewModel toastNotificationContainerViewModel,
        IServerAvailabilityProvider serverAvailabilityProvider)
    {
        _navigationService = navigationService;
       
        ToastNotificationContainerViewModel = toastNotificationContainerViewModel;

        _serverAvailabilitySubscription = 
            serverAvailabilityProvider.SubscribeToServerAvailability(OnServerAvailabilityChanged);
    
        _selectedNavigationItem = NavigationItems.First();
    }

    public ToastNotificationContainerViewModel ToastNotificationContainerViewModel { get; private set; }

    public string LoadingIndicatorText =>
        Loading ? _loadingText : _serverDownText;

    public override Visibility LoadingIndicatorVisibility => (Loading || ServerDown).ToVisibility();

    public IEnumerable<NavigationItem> NavigationItems => new List<NavigationItem>
    {
        new NavigationItem("Nalozi", NavigateToDashboard),
        new NavigationItem("Procedure", NavigateToProceduresView),
        new NavigationItem("Tehničari", NavigateToTechnicianMaintenanceView),
        new NavigationItem("Predlošci naloga", NavigateToCreateWarrantTemplateView),
        new NavigationItem("Zapisnik", NavigateToWarrantLogView)
    };

    public NavigationItem SelectedNavigationItem
    {
        get => _selectedNavigationItem;
        set
        {
            _selectedNavigationItem = value;
            value.NavigationAction.Invoke();
        }
    }

    [RelayCommand]
    public void NavigateToDashboard()
    {
        _navigationService.NavigateToView<DashboardView>();
    }

    public void NavigateToProceduresView()
    {
        _navigationService.NavigateToView<ProceduresView>();
    }

    public void NavigateToCreateWarrantTemplateView()
    {
        _navigationService.NavigateToView<WarrantTemplateMaintenanceView>();
    }

    public void NavigateToTechnicianMaintenanceView()
    {
        _navigationService.NavigateToView<TechnicianMaintenanceView>();
    }

    private void NavigateToWarrantLogView()
    {
        _navigationService.NavigateToView<WarrantLogView>();
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
