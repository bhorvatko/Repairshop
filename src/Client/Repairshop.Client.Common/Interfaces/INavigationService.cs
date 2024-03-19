using Repairshop.Client.Common.Navigation;

namespace Repairshop.Client.Common.Interfaces;

public interface INavigationService
{
    void NavigateToView<TView>() where TView : IViewBase;
}
