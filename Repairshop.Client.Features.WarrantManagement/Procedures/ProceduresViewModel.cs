using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;

public partial class ProceduresViewModel
    : ViewModelBase
{
    private readonly IProcedureService _procedureService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly IFormService _formService;

    [ObservableProperty]
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

    [RelayCommand]
    public async Task AddNewProcedure()
    {
        await _formService.ShowFormAsDialog<CreateProcedureView>();

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
}
