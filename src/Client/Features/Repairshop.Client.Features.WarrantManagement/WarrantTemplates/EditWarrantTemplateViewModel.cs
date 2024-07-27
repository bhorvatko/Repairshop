using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Validation;
using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Client.Features.WarrantManagement.Warrants;
using System.ComponentModel.DataAnnotations;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

public partial class EditWarrantTemplateViewModel
    : ObservableValidator
{
    private readonly IDialogService _dialogService;
    
    [ObservableProperty]
    [Required]
    public string _name = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Procedures))]
    [NotEmpty]
    public IEnumerable<WarrantStep> _steps =
        new List<WarrantStep>();

    public EditWarrantTemplateViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
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

    public bool Validate()
    {
        ValidateAllProperties();

        return !HasErrors;
    }
}
