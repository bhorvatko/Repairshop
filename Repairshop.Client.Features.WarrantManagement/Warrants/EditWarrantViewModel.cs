﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Validation;
using Repairshop.Client.Features.WarrantManagement.Procedures;
using Repairshop.Client.Features.WarrantManagement.WarrantTemplates;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public partial class EditWarrantViewModel
    : ObservableValidator
{
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    [Required(ErrorMessage = "Unesite subjekt naloga")]
    private string _subject = string.Empty;

    [ObservableProperty]
    private bool _isUrgent = false;

    [ObservableProperty]
    private WarrantStep? _currentStep;

    [ObservableProperty]
    private DateTime _deadlineDate = DateTime.Today;

    [ObservableProperty]
    private DateTime _deadlineTime = DateTime.Now;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentStepVisibility))]
    private bool _allowCurrentStepSelection;

    private string _number = 0.ToString();

    private IReadOnlyCollection<WarrantStep> _steps = new List<WarrantStep>();

    public EditWarrantViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    [NotEmpty]
    public IEnumerable<ProcedureSummaryViewModel> SequenceProcedures => Steps.Select(x => x.Procedure);

    public IReadOnlyCollection<WarrantStep> Steps
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

    public DateTime Deadline
    {
        get => DeadlineDate.Date + DeadlineTime.TimeOfDay;
        set
        {
            DeadlineDate = value.Date;
            DeadlineTime = new DateTime(value.TimeOfDay.Ticks);
        }
    }

    public string Number
    {
        get => _number;
        set
        {
            if (int.TryParse(value, out _))
            {
                SetProperty(ref _number, value);
            }
        }
    }

    public Visibility CurrentStepVisibility => 
        AllowCurrentStepSelection ? Visibility.Visible : Visibility.Collapsed;

    [RelayCommand]
    public async Task EditWarrantSequence()
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

    [RelayCommand]
    public async Task ApplyWarrantTemplate()
    {
        IEnumerable<WarrantStep>? sequence =
            await _dialogService.OpenDialog<
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
