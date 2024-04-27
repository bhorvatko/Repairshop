using Repairshop.Client.Infrastructure.Navigation;
using System.Windows.Controls;

namespace Repairshop.Client.WorkshopTerminal;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow 
    : MainView
{
    public MainWindow(MainViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

    public override ContentControl MainContentControl => contentControl;
}