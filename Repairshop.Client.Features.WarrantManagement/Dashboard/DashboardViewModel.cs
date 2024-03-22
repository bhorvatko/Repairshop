using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public partial class DashboardViewModel
    : ObservableObject, IViewModel
{
    private readonly ITechnicianService _technicianService;
    private readonly IWarrantService _warrantService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;

    private IEnumerable<TechnicianDashboardViewModel> _technicianDashboards = 
        Enumerable.Empty<TechnicianDashboardViewModel>();

    public DashboardViewModel(
        ITechnicianService technicianService, 
        IWarrantService warrantService,
        ILoadingIndicatorService loadingIndicatorService)
    {
        _technicianService = technicianService;
        _warrantService = warrantService;
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
        IEnumerable<WarrantViewModel> unassignedWarrants = Enumerable.Empty<WarrantViewModel>();

        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () => 
        {
            var techniciansTask = _technicianService.GetTechnicians();
            var unassignedWarrantsTask = _warrantService.GetUnassignedWarrants();

            technicians = await techniciansTask;
            unassignedWarrants = await unassignedWarrantsTask;
        });

        technicians = technicians.Append(TechnicianViewModel.CreateUnassignedTechnician(unassignedWarrants));

        TechnicianDashboards =
            Enumerable.Range(0, 3).Select(_ => new TechnicianDashboardViewModel(technicians));
    }
}
