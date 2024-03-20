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

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AvailableProcedures))]
    private IEnumerable<Procedure> _allProcedures = Enumerable.Empty<Procedure>();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AvailableProcedures))]
    private IEnumerable<WarrantStep> _steps = Enumerable.Empty<WarrantStep>();

    private Procedure? _selectedProcedure;

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

    public IEnumerable<Procedure> AvailableProcedures => AllProcedures.ExceptBy(Steps.Select(x => x.Procedure.Id), x => x.Id);
    public Procedure? SelectedProcedure { get => _selectedProcedure; set => SetProperty(ref _selectedProcedure, value); }

    [RelayCommand]
    public async Task LoadProcedures()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            AllProcedures = await _procedureService.GetProcedures();
        });
    }

    [RelayCommand]
    public void FinishEditing()
    {
        DialogFinished?.Invoke(Steps);
    }

    [RelayCommand]
    public void AddStep()
    {
        if (SelectedProcedure is null) return;

        Steps = Steps.Append(WarrantStep.CreateNew(SelectedProcedure));
    }
}
