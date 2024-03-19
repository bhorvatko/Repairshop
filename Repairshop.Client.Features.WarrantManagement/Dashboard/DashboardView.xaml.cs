using Repairshop.Client.Common.Navigation;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;
/// <summary>
/// Interaction logic for DashboardView.xaml
/// </summary>
public partial class DashboardView 
    : ViewBase<DashboardViewModel>
{
    public DashboardView(DashboardViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
