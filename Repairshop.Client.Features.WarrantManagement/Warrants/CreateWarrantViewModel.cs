using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public partial class CreateWarrantViewModel
    : ObservableObject, IFormViewModel
{
    private readonly IWarrantService _warrantService;
    private readonly IMessageDialogService _messageDialogService;
    private readonly INavigationService _navigationService;

    public CreateWarrantViewModel(
        EditWarrantViewModel editWarrantViewModel,
        IWarrantService warrantService,
        IMessageDialogService messageDialogService,
        INavigationService navigationService)
    {
        EditWarrantViewModel = editWarrantViewModel;
        _warrantService = warrantService;
        _messageDialogService = messageDialogService;
        _navigationService = navigationService;
    }

    public EditWarrantViewModel EditWarrantViewModel { get; private set; }

    public async Task CreateWarrant()
    {
        await _warrantService.CreateWarrant(
            EditWarrantViewModel.Subject,
            EditWarrantViewModel.Deadline,
            EditWarrantViewModel.IsUrgent,
            EditWarrantViewModel.Steps
                .Select(x => new CreateWarrantStepDto()
                {
                    CanBeTransitionedToByFrontDesk = x.CanBeTransitionedToByFrontDesk,
                    CanBeTransitionedToByWorkshop = x.CanBeTransitionedToByWorkshop,
                    ProcedureId = x.Procedure.Id!.Value
                }));

        _messageDialogService.ShowMessage(
            "Uspjeh!",
            "Novi nalog uspješno kreiran!");

        _navigationService.NavigateToView<DashboardView>();
    }

    public string GetSubmitText() => "Kreiraj nalog";

    public Task SubmitForm() => CreateWarrant();

    public bool ValidateForm()
    {
        return EditWarrantViewModel.Validate();
    }
}
