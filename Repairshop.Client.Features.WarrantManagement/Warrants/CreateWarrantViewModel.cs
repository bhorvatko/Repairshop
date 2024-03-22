using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public partial class CreateWarrantViewModel
    : IViewModel
{
    private readonly IWarrantService _warrantService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly IMessageDialogService _messageDialogService;
    private readonly INavigationService _navigationService;

    public CreateWarrantViewModel(
        EditWarrantViewModel editWarrantViewModel,
        IWarrantService warrantService,
        ILoadingIndicatorService loadingIndicatorService,
        IMessageDialogService messageDialogService,
        INavigationService navigationService)
    {
        EditWarrantViewModel = editWarrantViewModel;
        _warrantService = warrantService;
        _loadingIndicatorService = loadingIndicatorService;
        _messageDialogService = messageDialogService;
        _navigationService = navigationService;
    }

    public EditWarrantViewModel EditWarrantViewModel { get; private set; }

    [RelayCommand]
    public async Task CreateWarrant()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
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
        });
    }
}
