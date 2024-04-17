using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public class WarrantPreviewControlViewModelFactory
{
    private readonly INavigationService _navigationService;
    private readonly IWarrantService _warrantService;

    public WarrantPreviewControlViewModelFactory(
        INavigationService navigationService,
        IWarrantService warrantService)
    {
        _navigationService = navigationService;
        _warrantService = warrantService;
    }

    public WarrantPreviewControlViewModel CreateViewModel(
        WarrantSummaryViewModel warrant)
    {
        return new WarrantPreviewControlViewModel(
            warrant,
            _navigationService,
            _warrantService);
    }
}
