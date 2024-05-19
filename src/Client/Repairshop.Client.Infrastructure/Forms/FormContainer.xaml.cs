using Repairshop.Client.Common.Extensions;
using Repairshop.Client.Common.Forms;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace Repairshop.Client.Infrastructure.Forms;
/// <summary>
/// Interaction logic for FormContainer.xaml
/// </summary>
public partial class FormContainer 
    : UserControl, INotifyPropertyChanged
{
    private IFormViewModel? _viewModel;

    public FormContainer()
    {
        InitializeComponent();
    }

    public bool SubmissionInProgress { get; private set; }
    public bool EnableForm => !SubmissionInProgress;
    public Visibility ProgressBarVisibility => SubmissionInProgress.ToVisibility();

    public string SubmitText =>
        _viewModel?.GetSubmitText() ?? string.Empty;

    public FormBase? FormContent
    {
        get => (FormBase)FormContentControl.Content;
        set
        {
            FormContentControl.Content = value;

            _viewModel = value?.ViewModel;

            PropertyChanged?.Invoke(this, new(nameof(SubmitText)));
            PropertyChanged?.Invoke(this, new(nameof(FormContent)));
        }
    }

    public Action? OnSubmissionFinished { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private async void Submit(object sender, RoutedEventArgs e)
    {
        if (_viewModel?.ValidateForm() != true) return;

        SetSubmissionInProgress(true);

        await _viewModel!.SubmitForm();

        SetSubmissionInProgress(false);

        OnSubmissionFinished?.Invoke();
    }

    private void SetSubmissionInProgress(bool value)
    {
        SubmissionInProgress = value;
        PropertyChanged?.Invoke(this, new(nameof(SubmissionInProgress)));
        PropertyChanged?.Invoke(this, new(nameof(ProgressBarVisibility)));
        PropertyChanged?.Invoke(this, new(nameof(EnableForm)));
    }
}
