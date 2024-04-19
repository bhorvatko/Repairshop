using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Warrants;
using System.Windows;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public partial class TechnicianDashboardViewModel
    : ObservableObject, IDisposable
{
    private readonly WarrantPreviewControlViewModelFactory _warrantPreviewControlViewModelFactory;
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly ITechnicianService _technicianService;
    private readonly INavigationService _navigationService;
    private readonly IWarrantService _warrantService;
    private readonly IWarrantNotificationService _warrantNotificationService;

    private IDisposable? _warrantAddedSubscription;

    [ObservableProperty]
    private IEnumerable<TechnicianViewModel> _availableTechnicians = 
        Enumerable.Empty<TechnicianViewModel>();

    [ObservableProperty]
    private IEnumerable<WarrantPreviewControlViewModel> _warrants = 
        Enumerable.Empty<WarrantPreviewControlViewModel>();

    private TechnicianViewModel? _selectedTechnician = null;

    public TechnicianDashboardViewModel(
        WarrantPreviewControlViewModelFactory warrantPreviewControlViewModelFactory,
        ILoadingIndicatorService loadingIndicatorService,
        ITechnicianService technicianService,
        INavigationService navigationService,
        IWarrantService warrantService,
        IWarrantNotificationService warrantNotificationService,
        IReadOnlyCollection<TechnicianViewModel> availableTechnicians,
        Guid? selectedTechnicianId)
    {
        _warrantPreviewControlViewModelFactory = warrantPreviewControlViewModelFactory;
        _loadingIndicatorService = loadingIndicatorService;
        _technicianService = technicianService;
        _navigationService = navigationService;
        _warrantService = warrantService;
        _warrantNotificationService = warrantNotificationService;

        AvailableTechnicians = availableTechnicians;

        SelectedTechnician =
            availableTechnicians.FirstOrDefault(x => x.Id == selectedTechnicianId);
    }

    public TechnicianViewModel? SelectedTechnician
    {
        get => _selectedTechnician;
        set
        {
            SetProperty(ref _selectedTechnician, value);

            Warrants = value?
                .Warrants
                .Select(CreateWarrantPreviewControlViewModel)
                ?? Enumerable.Empty<WarrantPreviewControlViewModel>();

            _warrantAddedSubscription?.Dispose();

            _warrantAddedSubscription =
                _warrantNotificationService.SubscribeToWarrantAddedNotifications(
                    value?.Id,
                    OnWarrantAdded);
        }
    }

    [RelayCommand]
    public async Task WarrantDrop(DragEventArgs e)
    {
        if (!e.Data.GetDataPresent(typeof(WarrantSummaryViewModel)))
        {
            return;
        }

        WarrantSummaryViewModel warrant =
            (WarrantSummaryViewModel)e.Data.GetData(typeof(WarrantSummaryViewModel));

        await _loadingIndicatorService.ShowLoadingIndicatorForAction(() => 
            SelectedTechnician?.Id is not null
                ? _technicianService.AssignWarrant(SelectedTechnician.Id.Value, warrant.Id)
                : _warrantService.UnassignWarrant(warrant.Id));

        _navigationService.NavigateToView<DashboardView>();
    }

    public void Dispose()
    {
        _warrantAddedSubscription?.Dispose();
    }

    private void OnWarrantAdded(WarrantSummaryViewModel addedWarrant) =>
        Warrants = Warrants.Append(CreateWarrantPreviewControlViewModel(addedWarrant));

    private WarrantPreviewControlViewModel CreateWarrantPreviewControlViewModel(WarrantSummaryViewModel warrant) =>
        _warrantPreviewControlViewModelFactory.CreateViewModel(warrant);
}
