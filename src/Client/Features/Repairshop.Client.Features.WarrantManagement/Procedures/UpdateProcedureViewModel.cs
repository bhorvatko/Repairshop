using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Common.Forms;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;

public partial class UpdateProcedureViewModel
    : ObservableObject, IFormViewModel
{
    private readonly IProcedureService _procedureService;

    [ObservableProperty]
    private EditProcedureViewModel _editProcedureViewModel;

    [ObservableProperty]
    private Guid _procedureId;

    public UpdateProcedureViewModel(IProcedureService procedureService)
    {
        _procedureService = procedureService;

        EditProcedureViewModel = new EditProcedureViewModel();
    }

    public string GetSubmitText() => "AŽURIRAJ PROCEDURU";

    public async Task SubmitForm()
    {
        await _procedureService.UpdateProcedure(
            ProcedureId,
            EditProcedureViewModel.Name,
            EditProcedureViewModel.GetColorAsRgb());
    }

    public bool ValidateForm() =>
        EditProcedureViewModel.Validate();

}
