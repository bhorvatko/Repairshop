using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;
using System.Windows;

namespace Repairshop.Client.FrontDesk;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(
        MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}