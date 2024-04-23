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
    private IDisposable? _warrantRemovedSubscription;
    private IDisposable? _warrantUpdatedSubscription;

    [ObservableProperty]
    private IEnumerable<TechnicianViewModel> _availableTechnicians = 
        Enumerable.Empty<TechnicianViewModel>();

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

            SetSubscriptions(value?.Id);
        }
    }

    public IEnumerable<WarrantPreviewControlViewModel> Warrants
    {
        get => _warrants
            .OrderBy(x => x.Warrant.IsUrgent)
            .ThenBy(x => x.Warrant.Deadline);

        set => SetProperty(ref _warrants, value);
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

        if (warrant.TechnicianId == SelectedTechnician?.Id)
        {
            return;
        }

        await _loadingIndicatorService.ShowLoadingIndicatorForAction(() => 
            SelectedTechnician?.Id is not null
                ? _technicianService.AssignWarrant(SelectedTechnician.Id.Value, warrant.Id)
                : _warrantService.UnassignWarrant(warrant.Id));

        _navigationService.NavigateToView<DashboardView>();
    }

    public void Dispose()
    {
        _warrantAddedSubscription?.Dispose();
        _warrantRemovedSubscription?.Dispose();

        foreach (WarrantPreviewControlViewModel warrant in Warrants)
        {
            warrant.Dispose();
        }   
    }

    private void OnWarrantAdded(WarrantSummaryViewModel addedWarrant)
    {
        WarrantPreviewControlViewModel addedWarrantViewModel =
            CreateWarrantPreviewControlViewModel(addedWarrant);

        addedWarrantViewModel.PlayUpdateAnimation = true;

        Warrants = Warrants.Append(addedWarrantViewModel);
    }

    private void OnWarrantRemoved(Guid removedWarrantId)
    {
        WarrantPreviewControlViewModel? warrantToBeRemoved = 
            Warrants.FirstOrDefault(x => x.Warrant.Id == removedWarrantId);

        if (warrantToBeRemoved is null) return;

        warrantToBeRemoved.Dispose();

        Warrants = Warrants.Except(new[] { warrantToBeRemoved });
    }

    private void OnWarrantUpdated(WarrantSummaryViewModel updatedWarrant)
    {
        WarrantPreviewControlViewModel updatedWarrantViewModel =
            CreateWarrantPreviewControlViewModel(updatedWarrant);

        updatedWarrantViewModel.PlayUpdateAnimation = true;

        Warrants = Warrants
            .Where(x => x.Warrant.Id != updatedWarrant.Id)
            .Append(updatedWarrantViewModel);
    }

    private WarrantPreviewControlViewModel CreateWarrantPreviewControlViewModel(WarrantSummaryViewModel warrant) =>
        _warrantPreviewControlViewModelFactory.CreateViewModel(warrant);

    private void SetSubscriptions(Guid? technicianId)
    {
        _warrantAddedSubscription?.Dispose();
        _warrantRemovedSubscription?.Dispose();
        _warrantUpdatedSubscription?.Dispose();

        _warrantAddedSubscription =
            _warrantNotificationService.SubscribeToWarrantAddedNotifications(
                technicianId,
                OnWarrantAdded);

        _warrantRemovedSubscription =
            _warrantNotificationService.SubscribeToWarrantRemovedNotifications(
                technicianId,
                OnWarrantRemoved);

        _warrantUpdatedSubscription =
            _warrantNotificationService.SubscribeToWarrantUpdatedNotifications(
                technicianId,
                OnWarrantUpdated);
    }
}
