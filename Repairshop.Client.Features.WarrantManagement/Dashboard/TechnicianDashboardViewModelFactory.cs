using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public class TechnicianDashboardViewModelFactory
{
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly ITechnicianService _technicianService;
    private readonly IWarrantService _warrantService;
    private readonly WarrantPreviewControlViewModelFactory _warrantPreviewControlViewModelFactory;
    private readonly INavigationService _navigationService;

    public TechnicianDashboardViewModelFactory(
        ILoadingIndicatorService loadingIndicatorService, 
        ITechnicianService technicianService, 
        IWarrantService warrantService, 
        WarrantPreviewControlViewModelFactory warrantPreviewControlViewModelFactory,
        INavigationService navigationService)
    {
        _loadingIndicatorService = loadingIndicatorService;
        _technicianService = technicianService;
        _warrantService = warrantService;
        _warrantPreviewControlViewModelFactory = warrantPreviewControlViewModelFactory;
        _navigationService = navigationService;
    }

    public async Task<IReadOnlyCollection<TechnicianDashboardViewModel>> CreateViewModel(
        IEnumerable<Guid?> technicianIds)
    {
        IEnumerable<TechnicianViewModel> technicians = 
            Enumerable.Empty<TechnicianViewModel>();

        IEnumerable<WarrantSummaryViewModel> unassignedWarrants = 
            Enumerable.Empty<WarrantSummaryViewModel>();

        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            var techniciansTask = _technicianService.GetTechnicians();
            var unassignedWarrantsTask = _warrantService.GetUnassignedWarrants();

            technicians = await techniciansTask;
            unassignedWarrants = await unassignedWarrantsTask;
        });

        technicians = 
            technicians.Append(TechnicianViewModel.CreateUnassignedTechnician(unassignedWarrants));

        return technicianIds
            .Select(technicianId => 
                new TechnicianDashboardViewModel(
                    _warrantPreviewControlViewModelFactory, 
                    _loadingIndicatorService,
                    _technicianService,
                    _navigationService,
                    _warrantService,
                    technicians.ToList(),
                    technicianId))
            .ToList();
    }
}
