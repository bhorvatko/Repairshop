using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Client.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

public partial class CreateWarrantTemplateViewModel
    : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly IWarrantTemplateService _warrantTemplateService;
    private readonly IMessageDialogService _messageDialogService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public string _name = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Procedures))]
    public IEnumerable<WarrantStep> _steps =
        new List<WarrantStep>();

    public CreateWarrantTemplateViewModel(
        IDialogService dialogService,
        ILoadingIndicatorService loadingIndicatorService,
        IWarrantTemplateService warrantTemplateService,
        IMessageDialogService messageDialogService,
        INavigationService navigationService)
    {
        _dialogService = dialogService;
        _loadingIndicatorService = loadingIndicatorService;
        _warrantTemplateService = warrantTemplateService;
        _messageDialogService = messageDialogService;
        _navigationService = navigationService;
    }

    public IEnumerable<ProcedureSummaryViewModel> Procedures =>
        Steps.Select(x => x.Procedure);

    [RelayCommand]
    public void EditStepSequence()
    {
        IEnumerable<WarrantStep>? sequence =
            _dialogService.OpenDialog<
                EditWarrantSequenceView,
                EditWarrantSequenceViewModel,
                IEnumerable<WarrantStep>>(
                vm =>
                {
                    vm.Steps = Steps.ToList();
                });

        if (sequence is not null)
        {
            Steps = sequence.ToList();
        }
    }

    [RelayCommand]
    public async Task CreateWarrantTemplate()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            await _warrantTemplateService.CreateWarrantTemplate(
                Name,
                Steps.Select(x => new CreateWarrantStepDto()
                {
                    CanBeTransitionedToByFrontDesk = x.CanBeTransitionedToByFrontDesk,
                    CanBeTransitionedToByWorkshop = x.CanBeTransitionedToByWorkshop,
                    ProcedureId = x.Procedure.Id!.Value
                }));

            _messageDialogService.ShowMessage(
                "Uspjeh!",
                "Nova šablona uspješno kreirana!");

            _navigationService.NavigateToView<DashboardView>();
        });
    }
}
