using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public partial class DashboardViewModel
    : ObservableObject, IViewModel
{
    private readonly TechnicianDashboardViewModelFactory _technicianDashboardViewModelFactory;
    private IEnumerable<TechnicianDashboardViewModel> _technicianDashboards = 
        Enumerable.Empty<TechnicianDashboardViewModel>();

    public DashboardViewModel(
        TechnicianDashboardViewModelFactory technicianDashboardViewModelFactory)
    {
        _technicianDashboardViewModelFactory = technicianDashboardViewModelFactory;
    }

    public IEnumerable<TechnicianDashboardViewModel> TechnicianDashboards 
    { 
        get => _technicianDashboards; 
        set => SetProperty(ref _technicianDashboards, value); 
    }


    [RelayCommand]
    public async Task LoadTechnicians()
    {
        TechnicianDashboards = 
            await _technicianDashboardViewModelFactory.CreateViewModel(3);
    }
}
