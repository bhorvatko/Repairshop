using Repairshop.Client.Common.Forms;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;
/// <summary>
/// Interaction logic for UpdateWarrantView.xaml
/// </summary>
public partial class UpdateWarrantView
    : FormBase
{
    public UpdateWarrantView(UpdateWarrantViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
