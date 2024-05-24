using Repairshop.Client.Common.Forms;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;

/// <summary>
/// Interaction logic for UpdateProcedureView.xaml
/// </summary>
public partial class UpdateProcedureView : FormBase
{
    public UpdateProcedureView(UpdateProcedureViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
