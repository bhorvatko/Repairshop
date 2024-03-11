using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public partial class DashboardViewModel
    : ObservableObject, IViewModel
{
    private readonly ITechnicianService _technicianService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private IEnumerable<TechnicianDashboardViewModel> _technicianDashboards = 
        Enumerable.Empty<TechnicianDashboardViewModel>();

    public DashboardViewModel(
        ITechnicianService technicianService, 
        ILoadingIndicatorService loadingIndicatorService)
    {
        _technicianService = technicianService;
        _loadingIndicatorService = loadingIndicatorService;
    }

    public IEnumerable<TechnicianDashboardViewModel> TechnicianDashboards 
    { 
        get => _technicianDashboards; 
        set => SetProperty(ref _technicianDashboards, value); 
    }


    [RelayCommand]
    public async Task LoadTechnicians()
    {
        IEnumerable<TechnicianViewModel> technicians = Enumerable.Empty<TechnicianViewModel>();

        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () => 
        {
            technicians = await _technicianService.GetTechnicians();
        });

        TechnicianDashboards =
            Enumerable.Range(0, 3).Select(_ => new TechnicianDashboardViewModel(technicians));
    }
}
