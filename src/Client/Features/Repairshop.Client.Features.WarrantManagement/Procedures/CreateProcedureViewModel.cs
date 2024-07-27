using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Common.Forms;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;

public partial class CreateProcedureViewModel
    : ObservableObject, IFormViewModel
{
    private readonly IProcedureService _procedureService;

    [ObservableProperty]
    private EditProcedureViewModel _editProcedureViewModel;

    public CreateProcedureViewModel(IProcedureService procedureService)
    {
        _procedureService = procedureService;

        EditProcedureViewModel = new EditProcedureViewModel();
    }

    public float Priority { get; set; }

    public string GetSubmitText() => "KREIRAJ PROCEDURU";

    public async Task SubmitForm()
    {
        await _procedureService.CreateProcedure(
            EditProcedureViewModel.Name,
            EditProcedureViewModel.GetColorAsRgb(),
            Priority);
    }

    public bool ValidateForm() =>
        EditProcedureViewModel.Validate();

}
