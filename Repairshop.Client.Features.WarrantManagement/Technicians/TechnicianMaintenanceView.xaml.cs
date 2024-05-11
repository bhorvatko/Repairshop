using Repairshop.Client.Common.Navigation;

namespace Repairshop.Client.Features.WarrantManagement.Technicians;
/// <summary>
/// Interaction logic for TechnicianMaintenanceView.xaml
/// </summary>
public partial class TechnicianMaintenanceView
    : ViewBase<TechnicianMaintenanceViewModel>
{
    public TechnicianMaintenanceView(TechnicianMaintenanceViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
