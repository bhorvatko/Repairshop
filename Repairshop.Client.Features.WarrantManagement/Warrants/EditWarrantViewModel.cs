using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public partial class EditWarrantViewModel
    : ObservableObject
{
    private readonly IDialogService _dialogService;

    private string _subject = string.Empty;
    private DateTime _deadline = DateTime.Now;
    private bool _isUrgent = false;
    private IEnumerable<WarrantStep> _steps = Enumerable.Empty<WarrantStep>();

    public EditWarrantViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public string Subject { get => _subject; set => SetProperty(ref _subject, value); }
    public DateTime Deadline { get => _deadline; set => SetProperty(ref _deadline, value); }
    public bool IsUrgent { get => _isUrgent; set => SetProperty(ref _isUrgent, value); }
    public IEnumerable<WarrantStep> Steps { get => _steps; set => SetProperty(ref _steps, value); }
    public IEnumerable<Procedure> SequenceProcedures => Steps.Select(x => x.Procedure);

    [RelayCommand]
    public void EditWarrantSequence()
    {
        IEnumerable<WarrantStep>? sequence = 
            _dialogService.OpenDialog<EditWarrantSequenceViewModel, IEnumerable<WarrantStep>>();

        if (sequence is not null)
        {
            Steps = sequence.ToList();
        }
    }
}
