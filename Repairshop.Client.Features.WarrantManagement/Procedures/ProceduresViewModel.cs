using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Extensions;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using System.Windows;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;

public partial class ProceduresViewModel
    : ViewModelBase
{
    private readonly IProcedureService _procedureService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly IFormService _formService;

    private IReadOnlyCollection<ProcedureViewModel> _procedures = 
        new List<ProcedureViewModel>();

    public ProceduresViewModel(
        IProcedureService procedureService,
        ILoadingIndicatorService loadingIndicatorService,
        IFormService formService)
    {
        _procedureService = procedureService;
        _loadingIndicatorService = loadingIndicatorService;
        _formService = formService;
    }

    public IReadOnlyCollection<ProcedureViewModel> Procedures
    {
        get => _procedures.OrderBy(p => p.Priority).ToList();
        set
        {
            SetProperty(ref _procedures, value.OrderBy(p => p.Priority).ToList());
            OnPropertyChanged(nameof(RowViewModels));
        }
    }

    public IEnumerable<ProcedureListRowViewModel> RowViewModels =>
        Procedures.Select(p => new ProcedureListRowViewModel()
        {
            Procedure = p,
            MoveDownButtonVisibility = (p != Procedures.Last()).ToVisibility(Visibility.Hidden),
            MoveUpButtonVisibility = (p != Procedures.First()).ToVisibility(Visibility.Hidden)
        });

    [RelayCommand]
    public async Task AddNewProcedure()
    {
        float greatestExistingPriority = Procedures.Any()
            ? Procedures.Select(p => p.Priority).Max()
            : 0;

        await _formService
            .ShowFormAsDialog<CreateProcedureView,CreateProcedureViewModel>(vm =>
            {
                vm.Priority = (float.MaxValue / 2 + greatestExistingPriority) / 2;
            });

        await LoadProcedures();
    }

    [RelayCommand]
    public async Task UpdateProcedure(ProcedureViewModel procedure)
    {
        await _formService
            .ShowFormAsDialog<
                UpdateProcedureView, 
                UpdateProcedureViewModel>(vm =>
                {
                    vm.ProcedureId = procedure.Id;
                    vm.EditProcedureViewModel.Name = procedure.Name;
                    vm.EditProcedureViewModel.BackgroundColor = procedure.BackgroundColor;
                });

        await LoadProcedures();
    }

    [RelayCommand]
    public async Task DeleteProcedure(ProcedureViewModel procedure)
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            await _procedureService.DeleteProcedure(procedure.Id);
            await LoadProcedures();
        });
    }

    [RelayCommand]
    public async Task LoadProcedures()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            Procedures = await _procedureService.GetProcedures();
        });
    }

    [RelayCommand]
    private async Task MoveProcedureUp(ProcedureViewModel procedure)
    {
        int procedureIndex = Procedures.ToList().IndexOf(procedure);

        // Cannot move procedure up if it's already at the top
        if (procedureIndex <= 0)
        {
            return;
        }

        float priorityOfPreviousElement =
            Procedures.ElementAtOrDefault(procedureIndex - 2)?.Priority ?? (float.MinValue / 2);

        float priorityOfNextElement =
            Procedures.ElementAtOrDefault(procedureIndex - 1)?.Priority ?? (float.MaxValue / 2);

        await SetProcedurePriority(priorityOfPreviousElement, priorityOfNextElement, procedure);
    }

    [RelayCommand]
    private async Task MoveProcedureDown(ProcedureViewModel procedure)
    {
        int procedureIndex = Procedures.ToList().IndexOf(procedure);

        // Cannot move procedure down if it's already at the bottom
        if (procedureIndex == Procedures.Count - 1)
        {
            return;
        }

        float priorityOfPreviousElement =
            Procedures.ElementAtOrDefault(procedureIndex + 1)?.Priority ?? (float.MinValue / 2);

        float priorityOfNextElement =
            Procedures.ElementAtOrDefault(procedureIndex + 2)?.Priority ?? (float.MaxValue / 2);

        await SetProcedurePriority(priorityOfPreviousElement, priorityOfNextElement, procedure);
    }

    private async Task SetProcedurePriority(
        float previousElememtPriority,
        float nextElementPriority,
        ProcedureViewModel procedure)
    {

        float newPriority = (nextElementPriority + previousElememtPriority) / 2;

        procedure.SetPriority(newPriority);

        OnPropertyChanged(nameof(RowViewModels));

        await _procedureService.SetProcedurePriority(procedure.Id, newPriority);
    }
}
