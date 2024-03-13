using System.Windows.Controls;

namespace Repairshop.Client.Common.Interfaces;

public interface IDialogService
{
    TResult? OpenDialog<TViewModel, TResult, TDialogContent>()
        where TViewModel : IDialogViewModel<TResult, TDialogContent>
        where TResult : class
        where TDialogContent : UserControl, IDialogContent;
}
