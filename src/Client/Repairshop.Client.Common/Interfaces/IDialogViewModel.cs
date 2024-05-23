using System.Windows.Controls;

namespace Repairshop.Client.Common.Interfaces;

public interface IDialogViewModel<TResult, TDialogContent>
    : IDialogViewModel<TResult>
    where TDialogContent : UserControl, IDialogContent
{
}

public interface IDialogViewModel<TResult>
    : IDialogViewModel
{
    public delegate void DialogFinishedEventHandler(TResult? result);

    event DialogFinishedEventHandler DialogFinished;
}

public interface IDialogViewModel
{
}