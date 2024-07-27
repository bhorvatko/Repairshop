﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public partial class EditWarrantSequenceViewModel
    : DialogViewModelBase<IEnumerable<WarrantStep>, EditWarrantSequenceView>
{
    private readonly IProcedureService _procedureService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AvailableProcedures))]
    private IEnumerable<ProcedureSummaryViewModel> _allProcedures = Enumerable.Empty<ProcedureSummaryViewModel>();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AvailableProcedures))]
    private IReadOnlyCollection<WarrantStep> _steps = new List<WarrantStep>();

    private ProcedureSummaryViewModel? _selectedProcedure;

    public EditWarrantSequenceViewModel(
        IEnumerable<WarrantStep> initialSteps,
        IProcedureService procedureService, 
        ILoadingIndicatorService loadingIndicatorService)
    {
        _procedureService = procedureService;
        _loadingIndicatorService = loadingIndicatorService;

        Steps = new List<WarrantStep>(initialSteps);
    }

    public IEnumerable<ProcedureSummaryViewModel> AvailableProcedures => 
        AllProcedures
            .OrderBy(p => p.Priority)
            .ExceptBy(Steps.Select(x => x.Procedure.Id), x => x.Id);

    public ProcedureSummaryViewModel? SelectedProcedure { get => _selectedProcedure; set => SetProperty(ref _selectedProcedure, value); }

    [RelayCommand]
    public async Task LoadProcedures()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            AllProcedures = await _procedureService.GetProcedureSummaries();
        });
    }

    [RelayCommand]
    public void FinishEditing()
    {
        FinishDialog(Steps);
    }

    [RelayCommand]
    public void AddStep()
    {
        if (SelectedProcedure is null) return;

        Steps = Steps.Append(WarrantStep.CreateNew(SelectedProcedure)).ToList();
    }

    [RelayCommand]
    public void RemoveStep(WarrantStep step)
    {
        Steps = Steps.Where(x => x != step).ToList();
    }

    [RelayCommand]
    private void Cancel() => CancelDialog();
}