using Repairshop.Client.Common.Interfaces;

namespace Repairshop.Client.Infrastructure.LoadingIndicator;

public class LoadingIndicatorService
    : ILoadingIndicatorService
{
    private readonly IMainViewModel _mainViewModel;
    private readonly IMessageDialogService _messageDialogService;

    public LoadingIndicatorService(
        IMainViewModel mainViewModel,
        IMessageDialogService messageDialogService)
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
