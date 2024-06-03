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
        get => _logEntries.OrderByDescending(x => x.EventTime).ToList();
        set => SetProperty(ref _logEntries, value);
    }

    [RelayCommand]
    private Task OnLoaded() =>
        _loadingIndicatorService.ShowLoadingIndicatorForAction(LoadWarrantLog);

    private async Task LoadWarrantLog()
    {
        LogEntries = await _warrantLogService.GetWarrantLogEntries();
    }
}
