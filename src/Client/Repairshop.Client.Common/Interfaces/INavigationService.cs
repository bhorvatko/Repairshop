using Repairshop.Client.Common.Navigation;

namespace Repairshop.Client.Common.Interfaces;

public interface INavigationService
{
    void NavigateToView<TView>() where TView : IViewBase;

    void NavigateToView<TView, TViewModel>(Action<TViewModel> viewModelConfig)
        where TView : ViewBase<TViewModel>;
}
