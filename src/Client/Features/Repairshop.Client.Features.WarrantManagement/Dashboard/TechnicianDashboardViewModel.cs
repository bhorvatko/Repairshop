using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.UserNotifications;
using Repairshop.Client.Features.WarrantManagement.Configuration;
using Repairshop.Client.Features.WarrantManagement.Dashboard.WarrantFiltering;
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
    private readonly ISoundNotificationService _soundNotificationService;

    private IDisposable? _warrantAddedSubscription;
    private IDisposable? _warrantRemovedSubscription;
    private IDisposable? _warrantUpdatedSubscription;

    [ObservableProperty]
    private IEnumerable<TechnicianViewModel> _availableTechnicians = 
        Enumerable.Empty<TechnicianViewModel>();

    private IReadOnlyCollection<WarrantPreviewControlViewModel> _warrants = 
        new List<WarrantPreviewControlViewModel>();

    private TechnicianViewModel? _selectedTechnician = null;

    public TechnicianDashboardViewModel(
        WarrantPreviewControlViewModelFactory warrantPreviewControlViewModelFactory,
        ILoadingIndicatorService loadingIndicatorService,
        ITechnicianService technicianService,
        INavigationService navigationService,
        IWarrantService warrantService,
        IWarrantNotificationService warrantNotificationService,
        WarrantFilterSelectionViewModelFactory warrantFilterSelectionViewModelFactory,
        ISoundNotificationService soundNotificationService,
        IReadOnlyCollection<TechnicianViewModel> availableTechnicians,
        TechnicianDashboardConfiguration configuration)
    {
        _warrantPreviewControlViewModelFactory = warrantPreviewControlViewModelFactory;
        _loadingIndicatorService = loadingIndicatorService;
        _technicianService = technicianService;
        _navigationService = navigationService;
        _warrantService = warrantService;
        _warrantNotificationService = warrantNotificationService;
        _soundNotificationService = soundNotificationService;

        AvailableTechnicians = availableTechnicians;

        SelectedTechnician =
            availableTechnicians.FirstOrDefault(x => x.Id == configuration.TechnicianId);

        WarrantFilterSelectionViewModel = 
            warrantFilterSelectionViewModelFactory.Create(configuration.ProcedureFilters);
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
        }
    }

    public IEnumerable<WarrantPreviewControlViewModel> Warrants
    {
        get => _warrants
            .Where(x => !GetFilteredProcedureIds().Contains(x.Warrant.Procedure.Id))
            .OrderByDescending(x => x.Warrant.IsUrgent)
            .ThenBy(x => x.Warrant.Deadline);

        set => SetProperty(ref _warrants, value.ToList());
    }

    public WarrantFilterSelectionViewModel WarrantFilterSelectionViewModel { get; private set; }

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

    [RelayCommand]
    public void OnTabChanged()
    {
        // Refresh warrants when navigated back to warrants tab,
        // in case filters were changed in the filters tab
        OnPropertyChanged(nameof(Warrants));
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

    public IReadOnlyCollection<Guid> GetFilteredProcedureIds() =>
        WarrantFilterSelectionViewModel.FilteredProcedureIds;

    [RelayCommand]
    private async Task OnSelectedTechnicianChanged()
    {
        await SetSubscriptions(SelectedTechnician?.Id);
    }

    [RelayCommand]
    private async Task OnLoaded()
    {
        await SetSubscriptions(SelectedTechnician?.Id);
    }

    private async Task OnWarrantAdded(WarrantSummaryViewModel addedWarrant)
    {
        WarrantPreviewControlViewModel addedWarrantViewModel =
            CreateWarrantPreviewControlViewModel(addedWarrant);

        addedWarrantViewModel.PlayUpdateAnimation = true;

        Warrants = Warrants.Append(addedWarrantViewModel);

        await _soundNotificationService.PlaySoundNotification();
    }

    private Task OnWarrantRemoved(Guid removedWarrantId)
    {
        WarrantPreviewControlViewModel? warrantToBeRemoved = 
            Warrants.FirstOrDefault(x => x.Warrant.Id == removedWarrantId);

        if (warrantToBeRemoved is null) return Task.CompletedTask;

        warrantToBeRemoved.Dispose();

        Warrants = Warrants.Except(new[] { warrantToBeRemoved });

        return Task.CompletedTask;
    }

    private async Task OnWarrantUpdated(WarrantSummaryViewModel updatedWarrant)
    {
        WarrantPreviewControlViewModel updatedWarrantViewModel =
            CreateWarrantPreviewControlViewModel(updatedWarrant);

        updatedWarrantViewModel.PlayUpdateAnimation = true;

        Warrants = Warrants
            .Where(x => x.Warrant.Id != updatedWarrant.Id)
            .Append(updatedWarrantViewModel);

        await _soundNotificationService.PlaySoundNotification();
    }

    private WarrantPreviewControlViewModel CreateWarrantPreviewControlViewModel(WarrantSummaryViewModel warrant) =>
        _warrantPreviewControlViewModelFactory.CreateViewModel(warrant);

    private async Task SetSubscriptions(Guid? technicianId)
    {
        _warrantAddedSubscription?.Dispose();
        _warrantRemovedSubscription?.Dispose();
        _warrantUpdatedSubscription?.Dispose();

        _warrantAddedSubscription =
            await _warrantNotificationService.SubscribeToWarrantAddedNotifications(
                technicianId,
                OnWarrantAdded);

        _warrantRemovedSubscription =
            await _warrantNotificationService.SubscribeToWarrantRemovedNotifications(
                technicianId,
                OnWarrantRemoved);

        _warrantUpdatedSubscription =
            await _warrantNotificationService.SubscribeToWarrantUpdatedNotifications(
                technicianId,
                OnWarrantUpdated);
    }
}
