using Repairshop.Client.Common.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace Repairshop.Client.Infrastructure.Navigation;
/// <summary>
/// Interaction logic for DialogContainer.xaml
/// </summary>
public partial class DialogContainer
    : Window
{
    private object? _viewModel;

    public DialogContainer()
    {
        InitializeComponent();
    }

    public object? Result { get; private set; }

    public void ShowWithContent<TResult>(UserControl dialogContent)
    {
        DialogContentControl.Content = dialogContent;
        _viewModel = dialogContent.DataContext;

        ((IDialogViewModel<TResult>)_viewModel).DialogFinished += FinishDialog;

        ShowDialog();
    }

    private void FinishDialog<TResult>(TResult result)
    {
        Result = result;

        if (_viewModel is not null)
        {
            ((IDialogViewModel<TResult>)_viewModel).DialogFinished -= FinishDialog;
        }

        Close();
    }
}
