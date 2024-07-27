using Repairshop.Client.Common.Forms;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

/// <summary>
/// Interaction logic for UpdateWarrantTemplateView.xaml
/// </summary>
public partial class UpdateWarrantTemplateView
    : FormBase
{
    public UpdateWarrantTemplateView(UpdateWarrantTemplateViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
