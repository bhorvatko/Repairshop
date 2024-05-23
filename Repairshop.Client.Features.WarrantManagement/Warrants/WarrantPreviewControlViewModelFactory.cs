using Repairshop.Client.Common.ClientContext;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public class WarrantPreviewControlViewModelFactory
{
    private readonly INavigationService _navigationService;
    private readonly IWarrantService _warrantService;
    private readonly IClientContextProvider _clientContextProvider;
    private readonly IFormService _formService;

    public WarrantPreviewControlViewModelFactory(
        INavigationService navigationService,
        IWarrantService warrantService,
        IClientContextProvider clientContextProvider,
        IFormService formService)
    {
        _navigationService = navigationService;
        _warrantService = warrantService;
        _clientContextProvider = clientContextProvider;
        _formService = formService;
    }

    public WarrantPreviewControlViewModel CreateViewModel(
        WarrantSummaryViewModel warrant)
    {
        return new WarrantPreviewControlViewModel(
            warrant,
            _navigationService,
            _warrantService,
            _clientContextProvider,
            _formService);
    }
}
