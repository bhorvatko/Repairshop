using Repairshop.Client.Common.Interfaces;
using System.Windows;

namespace Repairshop.Client.Infrastructure.Navigation;
/// <summary>
/// Interaction logic for DialogContainer.xaml
/// </summary>
public partial class DialogContainer
    : Window
{
    public DialogContainer(IDialogViewModel viewModel)
    {
        InitializeComponent();

        ViewModel = viewModel;

        viewModel.DialogFinished += FinishDialog;
    }

    private void FinishDialog(object result)
    {
        Result = result;

        ViewModel.DialogFinished -= FinishDialog;

        Close();
    }

    public object? Result { get; private set; }

    public IDialogViewModel ViewModel { get; }
}
