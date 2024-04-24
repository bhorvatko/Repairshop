using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Client.Features.WarrantManagement.Technicians;
using Repairshop.Client.Features.WarrantManagement.Warrants;
using Repairshop.Client.Features.WarrantManagement.WarrantTemplates;
using System.Windows;

namespace Repairshop.Client.FrontDesk;

public partial class MainViewModel
    : ObservableObject, IMainViewModel
{
    private readonly INavigationService _navigationService;

    private Visibility _loadingIndicatorVisibility = Visibility.Collapsed;

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public Visibility LoadingIndicatorVisibility { get => _loadingIndicatorVisibility; set => SetProperty(ref _loadingIndicatorVisibility, value); }

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
        _navigationService.NavigateToView<CreateWarrantView>();
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

    public void ShowLoadingIndicator()
    {
        LoadingIndicatorVisibility = Visibility.Visible;
    }

    public void HideLoadingIndicator()
    {
        LoadingIndicatorVisibility = Visibility.Collapsed;
    }
}
