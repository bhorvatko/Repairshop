using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Infrastructure.MessageDialog;

namespace Repairshop.Client.Infrastructure.LoadingIndicator;

public class LoadingIndicatorService
    : ILoadingIndicatorService
{
    private readonly IMainViewModel _mainViewModel;
    private readonly MessageDialogService _messageDialogService;

    public LoadingIndicatorService(
        IMainViewModel mainViewModel,
        MessageDialogService messageDialogService)
    {
        _mainViewModel = mainViewModel;
        _messageDialogService = messageDialogService;
    }

    public async Task ShowLoadingIndicatorForAction(Func<Task> task)
    {
        _mainViewModel.ShowLoadingIndicator();

        try
        {
            Task taskToExecute = task();

            await taskToExecute;
        }
        catch (Exception e)
        {
            _messageDialogService.ShowMessage(e);
        }
        finally
        {
            _mainViewModel.HideLoadingIndicator();
        }
    }
}
