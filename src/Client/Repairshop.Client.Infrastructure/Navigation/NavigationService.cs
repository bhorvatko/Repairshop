using Repairshop.Client.Common.Interfaces;
using System.Windows.Controls;

namespace Repairshop.Client.Infrastructure.Navigation;

public class NavigationService
    : INavigationService
{
    //private readonly Func<Type, IViewModel> _viewModelFactory;
    //private readonly Action<IViewModel> _setViewModelAction;

    //public NavigationService(
    //    Func<Type, IViewModel> viewModelFactory,
    //    Action<IViewModel> setViewModelAction)
    //{
    //    _viewModelFactory = viewModelFactory;
    //    _setViewModelAction = setViewModelAction;
    //}

    //public void NavigateToView<TViewModel>() where TViewModel : IViewModel
    //{
    //    IViewModel viewModel = _viewModelFactory(typeof(TViewModel));

    //    _setViewModelAction(viewModel);
    //}

    private readonly Func<Type, IViewModel> _viewModelFactory;
    private readonly ContentControl _mainWindowContentControl;

    public void NavigateToView<TViewModel>() where TViewModel : IViewModel
    {
        IViewModel viewModel = _viewModelFactory(typeof(TViewModel));



        _setViewModelAction(viewModel);
    }
}
