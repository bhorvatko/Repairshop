﻿using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;

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

        NavigateToView(view);
    }

    public void NavigateToView<TView, TViewModel>(Action<TViewModel> viewModelConfig)
        where TView : ViewBase<TViewModel>
    {
        TView view = _serviceProvider.GetRequiredService<TView>();

        viewModelConfig((TViewModel)view.DataContext);

        NavigateToView(view);
    }

    private void NavigateToView<TView>(TView view)
    {
        IViewBase? currentView = _mainView().MainContentControl?.Content as IViewBase;
        IViewModel? currentViewModel = currentView?.DataContext as IViewModel;

        currentViewModel?.OnNavigatedAway();

        _mainView().MainContentControl.Content = view;

        currentViewModel?.Dispose();
    }    
}
