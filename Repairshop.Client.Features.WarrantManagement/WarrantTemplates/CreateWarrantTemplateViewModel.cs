using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Validation;
using Repairshop.Client.Features.WarrantManagement.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Client.Features.WarrantManagement.Warrants;
using System.ComponentModel.DataAnnotations;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

public partial class CreateWarrantTemplateViewModel
    : ObservableValidator, IFormViewModel
{
    private readonly IDialogService _dialogService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly IWarrantTemplateService _warrantTemplateService;

    [ObservableProperty]
    [Required]
    public string _name = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Procedures))]
    [NotEmpty]
    public IEnumerable<WarrantStep> _steps =
        new List<WarrantStep>();

    public CreateWarrantTemplateViewModel(
        IDialogService dialogService,
        ILoadingIndicatorService loadingIndicatorService,
        IWarrantTemplateService warrantTemplateService)
    {
        _dialogService = dialogService;
        _loadingIndicatorService = loadingIndicatorService;
        _warrantTemplateService = warrantTemplateService;
    }

    public IEnumerable<ProcedureSummaryViewModel> Procedures =>
        Steps.Select(x => x.Procedure);

    [RelayCommand]
    public async Task EditStepSequence()
    {
        IEnumerable<WarrantStep>? sequence =
            await _dialogService.OpenDialog<
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

    public async Task SubmitForm()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(CreateWarrantTemplate);
    }

    public string GetSubmitText() => "KREIRAJ PREDLOŽAK";

    public bool ValidateForm()
    {
        ValidateAllProperties();

        return !HasErrors;
    }

    private async Task CreateWarrantTemplate()
    {
        await _warrantTemplateService.CreateWarrantTemplate(
            Name,
            Steps.Select(x => new CreateWarrantStepDto()
            {
                CanBeTransitionedToByFrontDesk = x.CanBeTransitionedToByFrontDesk,
                CanBeTransitionedToByWorkshop = x.CanBeTransitionedToByWorkshop,
                ProcedureId = x.Procedure.Id
            }));
    }
}
