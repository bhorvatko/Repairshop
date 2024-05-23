using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Common.Interfaces;
using System.Windows.Controls;

namespace Repairshop.Client.Common.Navigation;

public class DialogViewModelBase<TResult, TDialogContent>
    : ObservableObject, IDialogViewModel<TResult, TDialogContent> 
    where TDialogContent : UserControl, IDialogContent
{
    public event IDialogViewModel<TResult>.DialogFinishedEventHandler? DialogFinished;

    protected void FinishDialog(TResult result)
    {
        DialogFinished?.Invoke(result);
    }
}
