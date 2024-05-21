using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Features.WarrantManagement.Dashboard;

namespace Repairshop.Client.Features.WarrantManagement.Technicians;

public partial class UpdateTechnicianViewModel
    : ObservableObject, IFormViewModel
{
    private readonly ITechnicianService _technicianService;

    [ObservableProperty]
    private Guid _technicianId;

    public UpdateTechnicianViewModel(
        ITechnicianService technicianService,
        EditTechnicianViewModel editTechnicianViewModel)
    {
        _technicianService = technicianService;

        EditTechnicianViewModel = editTechnicianViewModel;
    }

    public EditTechnicianViewModel EditTechnicianViewModel { get; private set; }

    public string GetSubmitText() => "AŽURIRAJ TEHNIČARA";

    public Task SubmitForm() =>
        _technicianService
            .UpdateTechnician(
                TechnicianId, 
                EditTechnicianViewModel.Name);

    public bool ValidateForm() => EditTechnicianViewModel.Validate();
}
