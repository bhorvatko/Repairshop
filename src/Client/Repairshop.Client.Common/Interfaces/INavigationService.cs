namespace Repairshop.Client.Common.Interfaces;

public interface INavigationService
{
    void NavigateToView<TViewModel>() where TViewModel : IViewModel;
}
