using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public partial class EditWarrantSequenceViewModel
    : ObservableObject, IDialogViewModel<IEnumerable<WarrantStep>>
{
    private readonly IProcedureService _procedureService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;

    private IEnumerable<Procedure> _availableProcedures = Enumerable.Empty<Procedure>();
    private IEnumerable<WarrantStep> _steps = Enumerable.Empty<WarrantStep>();

    public EditWarrantSequenceViewModel(
        IEnumerable<WarrantStep> initialSteps,
        IProcedureService procedureService, 
        ILoadingIndicatorService loadingIndicatorService)
    {
        _procedureService = procedureService;
        _loadingIndicatorService = loadingIndicatorService;

        Steps = new List<WarrantStep>(initialSteps);
    }

    public IEnumerable<Procedure> AvailableProcedures { get => _availableProcedures; set => SetProperty(ref _availableProcedures, value); }
    public IEnumerable<WarrantStep> Steps { get => _steps; set => SetProperty(ref _steps, value); }

    public event DialogFinishedEventHandler? DialogFinished;

    [RelayCommand]
    public async Task LoadProcedures()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            _availableProcedures = await _procedureService.GetProcedures();
        });
    }
}
