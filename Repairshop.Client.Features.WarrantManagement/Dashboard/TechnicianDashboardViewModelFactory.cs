using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Configuration;
using Repairshop.Client.Features.WarrantManagement.Dashboard.WarrantFiltering;
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
    private readonly IWarrantNotificationService _warrantNotificationService;
    private readonly WarrantFilterSelectionViewModelFactory _warrantFilterSelectionViewModelFactory;

    public TechnicianDashboardViewModelFactory(
        ILoadingIndicatorService loadingIndicatorService, 
        ITechnicianService technicianService, 
        IWarrantService warrantService, 
        WarrantPreviewControlViewModelFactory warrantPreviewControlViewModelFactory,
        INavigationService navigationService,
        IWarrantNotificationService warrantNotificationService,
        WarrantFilterSelectionViewModelFactory warrantFilterSelectionViewModelFactory)
    {
        _loadingIndicatorService = loadingIndicatorService;
        _technicianService = technicianService;
        _warrantService = warrantService;
        _warrantPreviewControlViewModelFactory = warrantPreviewControlViewModelFactory;
        _navigationService = navigationService;
        _warrantNotificationService = warrantNotificationService;
        _warrantFilterSelectionViewModelFactory = warrantFilterSelectionViewModelFactory;
    }

    public async Task<IReadOnlyCollection<TechnicianDashboardViewModel>> CreateViewModels(
        IEnumerable<TechnicianDashboardConfiguration> configurations)
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

        return configurations
            .Select(configuration => 
                new TechnicianDashboardViewModel(
                    _warrantPreviewControlViewModelFactory, 
                    _loadingIndicatorService,
                    _technicianService,
                    _navigationService,
                    _warrantService,
                    _warrantNotificationService,
                    _warrantFilterSelectionViewModelFactory,
                    technicians.ToList(),
                    configuration))
            .ToList();
    }
}
