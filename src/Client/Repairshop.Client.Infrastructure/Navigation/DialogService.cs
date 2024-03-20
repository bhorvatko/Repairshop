using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;
using System.Windows.Controls;

namespace Repairshop.Client.Infrastructure.Navigation;

internal class DialogService
    : IDialogService
{
    private readonly IServiceProvider _serviceProvider;

    public DialogService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TResult? OpenDialog<TDialogContent, TResult>()
        where TResult : class
        where TDialogContent : UserControl, IDialogContent
    {
        DialogContainer dialogContainer = new DialogContainer();

        UserControl control = 
            _serviceProvider.GetRequiredService<TDialogContent>();

        dialogContainer.ShowWithContent<TResult>(control);

        return dialogContainer.Result as TResult;
    }

    public TResult? OpenDialog<TDialogContent, TViewModel, TResult>(Action<TViewModel> viewModelConfig)
        where TResult : class
        where TDialogContent : UserControl, IDialogContent
        where TViewModel : IDialogViewModel<TResult>
    {
        DialogContainer dialogContainer = new DialogContainer();

        UserControl control =
            _serviceProvider.GetRequiredService<TDialogContent>();

        viewModelConfig((TViewModel)control.DataContext);

        dialogContainer.ShowWithContent<TResult>(control);

        return dialogContainer.Result as TResult;
    }
}