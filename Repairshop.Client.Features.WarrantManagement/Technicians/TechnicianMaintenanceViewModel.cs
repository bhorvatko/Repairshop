using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using Repairshop.Client.Features.WarrantManagement.Dashboard;

namespace Repairshop.Client.Features.WarrantManagement.Technicians;

public partial class TechnicianMaintenanceViewModel
    : ViewModelBase
{
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly ITechnicianService _technicianService;
    private readonly IFormService _formService;
    private readonly IMessageDialogService _messageDialogService;

    [ObservableProperty]
    private IReadOnlyCollection<TechnicianViewModel> _technicians =
        new List<TechnicianViewModel>();

    public TechnicianMaintenanceViewModel(
        ILoadingIndicatorService loadingIndicatorService,
        ITechnicianService technicianService,
        IFormService formService,
        IMessageDialogService messageDialogService)
    {
        _loadingIndicatorService = loadingIndicatorService;
        _technicianService = technicianService;
        _formService = formService;
        _messageDialogService = messageDialogService;
    }

    [RelayCommand]
    private Task OnLoaded() => LoadTechnicians();

    [RelayCommand]
    private async Task OnEditTechnician(TechnicianViewModel technician)
    {
        _formService.ShowForm<UpdateTechnicianView, UpdateTechnicianViewModel>(viewModel =>
        {
            viewModel.TechnicianId = technician.Id!.Value;
            viewModel.EditTechnicianViewModel.Name = technician.Name;
        });

        await LoadTechnicians();
    }

    [RelayCommand]
    private async Task OnDeleteTechnician(TechnicianViewModel technician)
    {
        string confirmationMessage = "Jeste li sigurni da želite obrisati tehničara? Svi nalozi će biti premješteni u nedodjeljene";

        if (!_messageDialogService.GetConfirmation(confirmationMessage))
        {
            return;
        }

        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            await _technicianService.DeleteTechnician(technician.Id!.Value);
            await LoadTechnicians();
        });
    }

    [RelayCommand]
    private async Task OnAddTechnician()
    {
        _formService.ShowForm<AddTechnicianView>();

        await LoadTechnicians();
    }

    private async Task LoadTechnicians()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            Technicians = (await _technicianService.GetTechnicians()).ToList();
        });
    }
}
