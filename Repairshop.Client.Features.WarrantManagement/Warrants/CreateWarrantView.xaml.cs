using Repairshop.Client.Common.Forms;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;
/// <summary>
/// Interaction logic for CreateWarrantView.xaml
/// </summary>
public partial class CreateWarrantView
    : FormBase
{
    public CreateWarrantView(CreateWarrantViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
