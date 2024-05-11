using Repairshop.Client.Common.Forms;

namespace Repairshop.Client.Features.WarrantManagement.Technicians;
/// <summary>
/// Interaction logic for AddTechnicianView.xaml
/// </summary>
public partial class AddTechnicianView
    : FormBase
{
    public AddTechnicianView(AddTechnicianViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
