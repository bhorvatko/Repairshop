using Repairshop.Client.Common.Navigation;

namespace Repairshop.Client.Features.WarrantManagement.Technicians;
/// <summary>
/// Interaction logic for AddTechnicianView.xaml
/// </summary>
public partial class AddTechnicianView
    : ViewBase<AddTechnicianViewModel>
{
    public AddTechnicianView(AddTechnicianViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
