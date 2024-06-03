using Repairshop.Client.Common.Navigation;

namespace Repairshop.Client.Features.WarrantManagement.WarrantLog;
/// <summary>
/// Interaction logic for WarrantLogView.xaml
/// </summary>
public partial class WarrantLogView
    : ViewBase<WarrantLogViewModel>
{
    public WarrantLogView(WarrantLogViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
