using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public partial class UpdateWarrantViewModel
    : IFormViewModel
{
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly IWarrantService _warrantService;
    private readonly INavigationService _navigationService;

    public UpdateWarrantViewModel(
        EditWarrantViewModel editWarrantViewModel,
        ILoadingIndicatorService loadingIndicatorService, 
        IWarrantService warrantService, 
        INavigationService navigationService)
    {
        EditWarrantViewModel = editWarrantViewModel;
        _loadingIndicatorService = loadingIndicatorService;
        _warrantService = warrantService;
        _navigationService = navigationService;
    }

    public EditWarrantViewModel EditWarrantViewModel { get; private set; }
    public Guid WarrantId { get; set; }

    public string GetSubmitText() => "AŽURIRAJ NALOG";

    public async Task SubmitForm()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            await _warrantService.UpdateWarrant(
                WarrantId,
                EditWarrantViewModel.Subject,
                EditWarrantViewModel.Deadline,
                EditWarrantViewModel.IsUrgent,
                EditWarrantViewModel.Steps
                    .Select(x => new CreateWarrantStepDto()
                    {
                        CanBeTransitionedToByFrontDesk = x.CanBeTransitionedToByFrontDesk,
                        CanBeTransitionedToByWorkshop = x.CanBeTransitionedToByWorkshop,
                        ProcedureId = x.Procedure.Id
                    }),
                EditWarrantViewModel.CurrentStep?.Procedure.Id);

            _navigationService.NavigateToView<DashboardView>();
        });
    }

    public bool ValidateForm()
    {
        return EditWarrantViewModel.Validate();
    }
}
