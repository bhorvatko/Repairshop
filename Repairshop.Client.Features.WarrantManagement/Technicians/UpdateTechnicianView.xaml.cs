using Repairshop.Client.Common.Forms;

namespace Repairshop.Client.Features.WarrantManagement.Technicians;
/// <summary>
/// Interaction logic for UpdateTechnicianView.xaml
/// </summary>
public partial class UpdateTechnicianView 
    : FormBase
{
    public UpdateTechnicianView(UpdateTechnicianViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
