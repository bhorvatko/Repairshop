using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

public class UpdateWarrantTemplateViewModel
    : IFormViewModel
{
    private readonly ILoadingIndicatorService _loadingIndicatorService;
    private readonly IWarrantTemplateService _warrantTemplateService;

    public UpdateWarrantTemplateViewModel(
        ILoadingIndicatorService loadingIndicatorService,
        IWarrantTemplateService warrantTemplateService,
        EditWarrantTemplateViewModel editWarrantTemplateViewModel)
    {
        _loadingIndicatorService = loadingIndicatorService;
        _warrantTemplateService = warrantTemplateService;
        EditWarrantTemplateViewModel = editWarrantTemplateViewModel;
    }

    public EditWarrantTemplateViewModel EditWarrantTemplateViewModel { get; private set; }
    public Guid WarrantTemplateId { get; set; }

    public string GetSubmitText() => "AŽURIRAJ PREDLOŽAK";

    public async Task SubmitForm()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(UpdateWarrantTemplate);
    }

    public bool ValidateForm() => EditWarrantTemplateViewModel.Validate();

    private async Task UpdateWarrantTemplate()
    {
        await _warrantTemplateService.UpdateWarrantTemplate(
            WarrantTemplateId,
            EditWarrantTemplateViewModel.Name,
            EditWarrantTemplateViewModel.Steps
                .Select(x => new CreateWarrantStepDto()
                {
                    CanBeTransitionedToByFrontDesk = x.CanBeTransitionedToByFrontDesk,
                    CanBeTransitionedToByWorkshop = x.CanBeTransitionedToByWorkshop,
                    ProcedureId = x.Procedure.Id
                }));
    }
}
