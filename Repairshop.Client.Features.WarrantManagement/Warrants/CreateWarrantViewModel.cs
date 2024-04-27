using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.UserNotifications;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public partial class CreateWarrantViewModel
    : ObservableObject, IFormViewModel
{
    private readonly IWarrantService _warrantService;
    private readonly INavigationService _navigationService;
    private readonly IToastNotificationService _toastNotificationService;

    public CreateWarrantViewModel(
        EditWarrantViewModel editWarrantViewModel,
        IWarrantService warrantService,
        INavigationService navigationService,
        IToastNotificationService toastNotificationService)
    {
        EditWarrantViewModel = editWarrantViewModel;
        _warrantService = warrantService;
        _navigationService = navigationService;
        _toastNotificationService = toastNotificationService;
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

        _navigationService.NavigateToView<DashboardView>();

        await _toastNotificationService.ShowSuccess("Nalog uspješno kreiran!");
    }

    public string GetSubmitText() => "Kreiraj nalog";

    public Task SubmitForm() => CreateWarrant();

    public bool ValidateForm()
    {
        return EditWarrantViewModel.Validate();
    }
}
