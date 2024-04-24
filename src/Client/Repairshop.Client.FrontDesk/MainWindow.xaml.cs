using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Common.Navigation;
using Repairshop.Client.Infrastructure.Navigation;
using System.ComponentModel;
using System.Windows.Controls;

namespace Repairshop.Client.FrontDesk;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
    : MainView
{
    public MainWindow(
        MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public override ContentControl MainContentControl => contentControl;

    protected override void OnClosing(CancelEventArgs e)
    {
        IViewModel? currentViewModel = ((IViewBase)MainContentControl.Content).DataContext as IViewModel;

        currentViewModel?.OnNavigatedAway();

        base.OnClosing(e);
    }
}