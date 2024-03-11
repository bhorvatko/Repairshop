using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;
using System.Windows;

namespace Repairshop.Client.Infrastructure.Navigation;
internal class DialogService
    : IDialogService
{
    private readonly IServiceProvider _serviceProvider;

    public DialogService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TResult? OpenDialog<TViewModel, TResult>()
        where TViewModel : IDialogViewModel<TResult>
        where TResult : class
    {
        IDialogViewModel<TResult> viewModel = _serviceProvider.GetRequiredService<TViewModel>();

        Window dialogContainer = new DialogContainer(viewModel);

        dialogContainer.Show();

        return dialogContainer.DialogResult as TResult;
    }
}
