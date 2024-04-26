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
        await _viewModel!.SubmitForm();
        Close();
    }
}
