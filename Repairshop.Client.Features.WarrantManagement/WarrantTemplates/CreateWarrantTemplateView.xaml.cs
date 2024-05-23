using Repairshop.Client.Common.Forms;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;
/// <summary>
/// Interaction logic for CreateWarrantTemplateView.xaml
/// </summary>
public partial class CreateWarrantTemplateView 
    : FormBase
{
    public CreateWarrantTemplateView(CreateWarrantTemplateViewModel viewModel)
         : base(viewModel)
    {
        InitializeComponent();
    }
}
