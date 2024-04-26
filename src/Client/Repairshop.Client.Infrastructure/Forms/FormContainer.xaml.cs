using Repairshop.Client.Common.Extensions;
using Repairshop.Client.Common.Forms;
using System.ComponentModel;
using System.Windows;

namespace Repairshop.Client.Infrastructure.Forms;
/// <summary>
/// Interaction logic for FormContainer.xaml
/// </summary>
public partial class FormContainer 
    : Window, INotifyPropertyChanged
{
    private IFormViewModel? _viewModel;

    public FormContainer()
    {
        InitializeComponent();
    }

    public string SubmitText => 
        _viewModel?.GetSubmitText() ?? string.Empty;

    public bool SubmissionInProgress { get; private set; }
    public Visibility ProgressBarVisibility => SubmissionInProgress.ToVisibility();
    public bool EnableForm => !SubmissionInProgress;

    public event PropertyChangedEventHandler? PropertyChanged;

    public void ShowWithContent(FormBase formContent)
    {
        FormContentControl.Content = formContent;
        _viewModel = formContent.ViewModel;

        PropertyChanged?.Invoke(this, new(nameof(SubmitText)));

        ShowDialog();
    }

    private async void Submit(object sender, RoutedEventArgs e)
    {
        if (_viewModel?.ValidateForm() != true) return;

        SetSubmissionInProgress(true);

        await _viewModel!.SubmitForm();

        SetSubmissionInProgress(false);

        Close();
    }

    private void SetSubmissionInProgress(bool value)
    {
        SubmissionInProgress = value;
        PropertyChanged?.Invoke(this, new(nameof(SubmissionInProgress)));
        PropertyChanged?.Invoke(this, new(nameof(ProgressBarVisibility)));
        PropertyChanged?.Invoke(this, new(nameof(EnableForm)));
    }
}
