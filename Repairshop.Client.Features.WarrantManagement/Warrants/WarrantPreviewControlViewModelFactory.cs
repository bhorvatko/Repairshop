using Repairshop.Client.Common.ClientContext;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public class WarrantPreviewControlViewModelFactory
{
    private readonly INavigationService _navigationService;
    private readonly IWarrantService _warrantService;
    private readonly IClientContextProvider _clientContextProvider;

    public WarrantPreviewControlViewModelFactory(
        INavigationService navigationService,
        IWarrantService warrantService,
        IClientContextProvider clientContextProvider)
    {
        _navigationService = navigationService;
        _warrantService = warrantService;
        _clientContextProvider = clientContextProvider;
    }

    public WarrantPreviewControlViewModel CreateViewModel(
        WarrantSummaryViewModel warrant)
    {
        return new WarrantPreviewControlViewModel(
            warrant,
            _navigationService,
            _warrantService,
            _clientContextProvider);
    }
}
