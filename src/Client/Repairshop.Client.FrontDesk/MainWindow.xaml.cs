using Repairshop.Client.Infrastructure.Navigation;
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
}