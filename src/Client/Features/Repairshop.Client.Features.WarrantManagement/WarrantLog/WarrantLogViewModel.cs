using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.WarrantLog;

public partial class WarrantLogViewModel
    : ViewModelBase
{
    private readonly IWarrantLogService _warrantLogService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;

    [ObservableProperty]
    private bool _isFilterVisible;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LogEntries))]
    private string? _technicianNameFilter;

    private string? _warrantNumbeFilter;

    private IReadOnlyCollection<WarrantLogEntryViewModel> _logEntries
        = new List<WarrantLogEntryViewModel>();

    public WarrantLogViewModel(
        IWarrantLogService warrantLogService,
        ILoadingIndicatorService loadingIndicatorService)
    {
        _warrantLogService = warrantLogService;
        _loadingIndicatorService = loadingIndicatorService;
    }

    public IReadOnlyCollection<WarrantLogEntryViewModel> LogEntries
    {
        get => _logEntries
            .Where(x => string.IsNullOrEmpty(TechnicianNameFilter) 
                || x.TechnicianName?.ToLower().Contains(TechnicianNameFilter.ToLower()) == true)
            .Where(x => string.IsNullOrEmpty(WarrantNumberFilter)
                || x.WarrantNumber == int.Parse(WarrantNumberFilter))
            .OrderByDescending(x => x.EventTime)
            .ToList();
        set => SetProperty(ref _logEntries, value);
    }

    public string? WarrantNumberFilter
    {
        get => _warrantNumbeFilter;
        set
        {
            if (!int.TryParse(value, out _) && !string.IsNullOrEmpty(value)) return;

            SetProperty(ref _warrantNumbeFilter, string.IsNullOrEmpty(value) ? null : value);
            OnPropertyChanged(nameof(LogEntries));
        }
    }

    [RelayCommand]
    private Task OnLoaded() =>
        _loadingIndicatorService.ShowLoadingIndicatorForAction(LoadWarrantLog);

    [RelayCommand]
    private void ShowFilters() => IsFilterVisible = true;

    private async Task LoadWarrantLog()
    {
        LogEntries = await _warrantLogService.GetWarrantLogEntries();
    }
}
