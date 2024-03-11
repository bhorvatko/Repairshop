namespace Repairshop.Client.Common.Interfaces;

public interface IDialogService
{
    TResult? OpenDialog<TViewModel, TResult>()
        where TViewModel : IDialogViewModel<TResult>
        where TResult : class;
}
