using System.Windows.Controls;

namespace Repairshop.Client.Common.Interfaces;

public interface IDialogService
{
    TResult? OpenDialog<TDialogContent, TResult>()
        where TResult : class
        where TDialogContent : UserControl, IDialogContent;

    TResult? OpenDialog<TDialogContent, TViewModel, TResult>(Action<TViewModel> viewModelConfig)
        where TResult : class
        where TDialogContent : UserControl, IDialogContent
        where TViewModel : IDialogViewModel<TResult>;
}
