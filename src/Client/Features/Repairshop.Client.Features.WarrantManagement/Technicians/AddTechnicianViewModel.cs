using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Features.WarrantManagement.Dashboard;

namespace Repairshop.Client.Features.WarrantManagement.Technicians;

public partial class AddTechnicianViewModel
    : ObservableObject, IFormViewModel
{
    private readonly ITechnicianService _technicianService;

    public AddTechnicianViewModel(
        ITechnicianService technicianService,
        EditTechnicianViewModel editTechnicianViewModel)
    {
        _technicianService = technicianService;

        EditTechnicianViewModel = editTechnicianViewModel;
    }

    public EditTechnicianViewModel EditTechnicianViewModel { get; private set; }

    public string GetSubmitText() => "KREIRAJ TEHNIČARA";

    public Task SubmitForm() =>
        _technicianService
            .CreateTechnician(EditTechnicianViewModel.Name);

    public bool ValidateForm() => EditTechnicianViewModel.Validate();
}
