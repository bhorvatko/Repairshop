using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Client.Features.WarrantManagement.Technicians;
using Repairshop.Client.Features.WarrantManagement.Warrants;
using Repairshop.Client.Features.WarrantManagement.WarrantTemplates;
using Repairshop.Client.Infrastructure.Bootstrapping;
using Repairshop.Client.Infrastructure.UserNotifications;

namespace Repairshop.Client.FrontDesk;

public partial class MainViewModel
    : MainViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IFormService _formService;

    public MainViewModel(
        INavigationService navigationService,
        IFormService formService,
        ToastNotificationContainerViewModel toastNotificationContainerViewModel)
    {
        _navigationService = navigationService;
        _formService = formService;
       
        ToastNotificationContainerViewModel = toastNotificationContainerViewModel;
    }

    public ToastNotificationContainerViewModel ToastNotificationContainerViewModel { get; private set; }

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
}
