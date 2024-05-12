using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Repairshop.Client.Infrastructure.Navigation;

public abstract class MainView
    : Window
{
    public MainView(
        IMainViewModel viewModel)
    {
        DataContext = viewModel;

        Style = Application.Current.Resources["MaterialDesignWindow"] as Style;
    }

    public abstract ContentControl MainContentControl { get; }

    protected override void OnClosing(CancelEventArgs e)
    {
        IViewModel? currentViewModel = ((IViewBase)MainContentControl.Content).DataContext as IViewModel;

        currentViewModel?.OnNavigatedAway();

        base.OnClosing(e);
    }
}
