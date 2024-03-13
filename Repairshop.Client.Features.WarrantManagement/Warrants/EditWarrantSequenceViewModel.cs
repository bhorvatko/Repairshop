using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public partial class EditWarrantSequenceViewModel
    : ObservableObject, IDialogViewModel<IEnumerable<WarrantStep>, EditWarrantSequenceView>
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

    public event IDialogViewModel<IEnumerable<WarrantStep>>.DialogFinishedEventHandler? DialogFinished;

    public IEnumerable<Procedure> AvailableProcedures { get => _availableProcedures; set => SetProperty(ref _availableProcedures, value); }
    public IEnumerable<WarrantStep> Steps { get => _steps; set => SetProperty(ref _steps, value); }

    [RelayCommand]
    public async Task LoadProcedures()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            AvailableProcedures = await _procedureService.GetProcedures();
        });
    }

    [RelayCommand]
    public void FinishEditing()
    {
        DialogFinished?.Invoke(Steps);
    }
}
