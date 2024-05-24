using Repairshop.Client.Common.Forms;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;

/// <summary>
/// Interaction logic for CreateProcedureView.xaml
/// </summary>
public partial class CreateProcedureView : FormBase
{
    public CreateProcedureView(CreateProcedureViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
