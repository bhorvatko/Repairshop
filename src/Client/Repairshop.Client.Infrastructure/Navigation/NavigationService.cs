using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using System.Windows.Controls;

namespace Repairshop.Client.Infrastructure.Navigation;

internal class NavigationService
    : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Func<MainView> _mainView;

    public NavigationService(
        IServiceProvider serviceProvider,
        Func<MainView> mainView)
    {
        _serviceProvider = serviceProvider;
        _mainView = mainView;
    }

    public void NavigateToView<TView>() where TView : IViewBase
    {
        TView view = _serviceProvider.GetRequiredService<TView>();

        _mainView().MainContentControl.Content = view;
    }

    public void NavigateToView<TView, TViewModel>(Action<TViewModel> viewModelConfig)
        where TView : ViewBase<TViewModel>
    {
        TView view = _serviceProvider.GetRequiredService<TView>();

        viewModelConfig((TViewModel)view.DataContext);

        _mainView().MainContentControl.Content = view;
    }
}
