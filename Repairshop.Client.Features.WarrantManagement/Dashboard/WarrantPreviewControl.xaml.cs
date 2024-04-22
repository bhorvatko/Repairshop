using System.Windows.Controls;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;
/// <summary>
/// Interaction logic for WarrantPreviewControl.xaml
/// </summary>
public partial class WarrantPreviewControl : UserControl
{
    public WarrantPreviewControl()
    {
        InitializeComponent();
    }

    private void DoubleAnimation_Completed(object sender, EventArgs e)
    {
        WarrantPreviewControlViewModel? viewModel = 
            DataContext as WarrantPreviewControlViewModel;

        if (viewModel is not null) viewModel.PlayUpdateAnimation = false;
    }
}
