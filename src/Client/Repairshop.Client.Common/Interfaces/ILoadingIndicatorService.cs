namespace Repairshop.Client.Common.Interfaces;

public interface ILoadingIndicatorService
{
    Task ShowLoadingIndicatorForAction(Func<Task> task);
}
