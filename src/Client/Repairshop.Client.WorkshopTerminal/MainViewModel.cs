using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Infrastructure.Bootstrapping;

namespace Repairshop.Client.WorkshopTerminal;

public partial class MainViewModel
    : MainViewModelBase
{
    private readonly INavigationService _navigationService;

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public void OnLoaded()
    {
        _navigationService.NavigateToView<DashboardView>();
    }
}
