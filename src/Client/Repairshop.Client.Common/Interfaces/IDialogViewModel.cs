namespace Repairshop.Client.Common.Interfaces;

public interface IDialogViewModel<TResult>
    : IDialogViewModel
{
}

public interface IDialogViewModel
{
    event DialogFinishedEventHandler DialogFinished;
}

public delegate void DialogFinishedEventHandler(object result);
