using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;
using System.Windows.Controls;

namespace Repairshop.Client.Infrastructure.Navigation;

internal class DialogService
    : IDialogService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly DialogHostManager _dialogHostManager;

    public DialogService(
        IServiceProvider serviceProvider,
        DialogHostManager dialogHostManager)
    {
        _serviceProvider = serviceProvider;
        _dialogHostManager = dialogHostManager;
    }

    public Task<TResult?> OpenDialog<TDialogContent, TResult>()
        where TResult : class
        where TDialogContent : UserControl, IDialogContent =>
        OpenDialog<TDialogContent, TResult>(_ => { });


    public Task<TResult?> OpenDialog<TDialogContent, TViewModel, TResult>(Action<TViewModel> viewModelConfig)
        where TResult : class
        where TDialogContent : UserControl, IDialogContent
        where TViewModel : IDialogViewModel<TResult> =>
        OpenDialog<TDialogContent, TResult>(viewModel => viewModelConfig((TViewModel)viewModel));

    private async Task<TResult?> OpenDialog<TDialogContent, TResult>(Action<object> viewModelConfig)
        where TResult : class
        where TDialogContent : UserControl, IDialogContent
    {
        UserControl control =
            _serviceProvider.GetRequiredService<TDialogContent>();

        IDialogViewModel<TResult> viewModel = 
            (IDialogViewModel<TResult>)control.DataContext;

        viewModelConfig(viewModel);

        var taskCompletionSource = new TaskCompletionSource<TResult?>();

        viewModel.DialogFinished += result => taskCompletionSource.TrySetResult(result);

        _dialogHostManager.Show(control);

        TResult? result = await taskCompletionSource.Task;

        _dialogHostManager.Close();

        return result;
    }
}