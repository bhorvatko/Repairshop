using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Validation;
using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Client.Features.WarrantManagement.WarrantTemplates;
using System.ComponentModel.DataAnnotations;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public partial class EditWarrantViewModel
    : ObservableValidator
{
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    [Required]
    private string _subject = string.Empty;

    [ObservableProperty]
    [Required]
    private DateTime _deadline = DateTime.Now;

    [ObservableProperty]
    private bool _isUrgent = false;

    [ObservableProperty]
    private WarrantStep? _currentStep;

    private IEnumerable<WarrantStep> _steps = Enumerable.Empty<WarrantStep>();

    public EditWarrantViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    [NotEmpty]
    public IEnumerable<ProcedureSummaryViewModel> SequenceProcedures => Steps.Select(x => x.Procedure);

    public IEnumerable<WarrantStep> Steps
    {
        get => _steps;
        set
        {
            SetProperty(ref _steps, value);

            OnPropertyChanged(nameof(SequenceProcedures));

            if (!_steps.Any(s => s.Procedure.Id == CurrentStep?.Procedure.Id))
            {
                CurrentStep = _steps.FirstOrDefault();
            }
        }
    }

    [RelayCommand]
    public void EditWarrantSequence()
    {
        IEnumerable<WarrantStep>? sequence = 
            _dialogService.OpenDialog<
                EditWarrantSequenceView,
                EditWarrantSequenceViewModel,
                IEnumerable<WarrantStep>>(
                vm =>
                {
                    vm.Steps = Steps;
                });

        if (sequence is not null)
        {
            Steps = sequence.ToList();
        }
    }

    [RelayCommand]
    public void ApplyWarrantTemplate()
    {
        IEnumerable<WarrantStep>? sequence =
            _dialogService.OpenDialog<
                WarrantTemplateSelectorView,
                IEnumerable<WarrantStep>>();

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
