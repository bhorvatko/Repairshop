using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public partial class UpdateWarrantViewModel
    : IViewModel
{
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly IWarrantService _warrantService;
    private readonly IMessageDialogService _messageDialogService;
    private readonly INavigationService _navigationService;

    public UpdateWarrantViewModel(
        EditWarrantViewModel editWarrantViewModel,
        ILoadingIndicatorService loadingIndicatorService, 
        IWarrantService warrantService, 
        IMessageDialogService messageDialogService, 
        INavigationService navigationService)
    {
        EditWarrantViewModel = editWarrantViewModel;
        _loadingIndicatorService = loadingIndicatorService;
        _warrantService = warrantService;
        _messageDialogService = messageDialogService;
        _navigationService = navigationService;
    }

    public EditWarrantViewModel EditWarrantViewModel { get; private set; }
    public Guid WarrantId { get; set; }

    [RelayCommand]
    public async Task UpdateWarrant()
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
                        ProcedureId = x.Procedure.Id!.Value
                    }),
                EditWarrantViewModel.CurrentStep?.Procedure.Id);

            _messageDialogService.ShowMessage(
                "Uspjeh!",
                "Nalog uspješno ažuriran!");

            _navigationService.NavigateToView<DashboardView>();
        });
    }
}
