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

    public TechnicianDashboardViewModelFactory(
        ILoadingIndicatorService loadingIndicatorService, 
        ITechnicianService technicianService, 
        IWarrantService warrantService, 
        WarrantPreviewControlViewModelFactory warrantPreviewControlViewModelFactory)
    {
        _loadingIndicatorService = loadingIndicatorService;
        _technicianService = technicianService;
        _warrantService = warrantService;
        _warrantPreviewControlViewModelFactory = warrantPreviewControlViewModelFactory;
    }

    public async Task<IEnumerable<TechnicianDashboardViewModel>> CreateViewModel(int count)
    {
        IEnumerable<TechnicianViewModel> technicians = Enumerable.Empty<TechnicianViewModel>();
        IEnumerable<WarrantSummaryViewModel> unassignedWarrants = Enumerable.Empty<WarrantSummaryViewModel>();

        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            var techniciansTask = _technicianService.GetTechnicians();
            var unassignedWarrantsTask = _warrantService.GetUnassignedWarrants();

            technicians = await techniciansTask;
            unassignedWarrants = await unassignedWarrantsTask;
        });

        technicians = technicians.Append(TechnicianViewModel.CreateUnassignedTechnician(unassignedWarrants));

        return Enumerable
            .Range(0, count)
            .Select(_ => 
                new TechnicianDashboardViewModel(
                    _warrantPreviewControlViewModelFactory, 
                    technicians));
    }
}
