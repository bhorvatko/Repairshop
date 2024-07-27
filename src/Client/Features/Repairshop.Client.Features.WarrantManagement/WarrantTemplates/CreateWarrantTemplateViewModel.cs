using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

public partial class CreateWarrantTemplateViewModel
    : IFormViewModel
{
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly IWarrantTemplateService _warrantTemplateService;

    public CreateWarrantTemplateViewModel(
        ILoadingIndicatorService loadingIndicatorService,
        IWarrantTemplateService warrantTemplateService,
        EditWarrantTemplateViewModel editWarrantTemplateViewModel)
    {
        _loadingIndicatorService = loadingIndicatorService;
        _warrantTemplateService = warrantTemplateService;

        EditWarrantTemplateViewModel = editWarrantTemplateViewModel;
    }

    public EditWarrantTemplateViewModel EditWarrantTemplateViewModel { get; private set; }

    public async Task SubmitForm()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(CreateWarrantTemplate);
    }

    public string GetSubmitText() => "KREIRAJ PREDLOŽAK";

    public bool ValidateForm() => EditWarrantTemplateViewModel.Validate();

    private async Task CreateWarrantTemplate()
    {
        await _warrantTemplateService.CreateWarrantTemplate(
            EditWarrantTemplateViewModel.Name,
            EditWarrantTemplateViewModel
                .Steps
                .Select(x => new CreateWarrantStepDto()
                {
                    CanBeTransitionedToByFrontDesk = x.CanBeTransitionedToByFrontDesk,
                    CanBeTransitionedToByWorkshop = x.CanBeTransitionedToByWorkshop,
                    ProcedureId = x.Procedure.Id
                }));
    }
}
