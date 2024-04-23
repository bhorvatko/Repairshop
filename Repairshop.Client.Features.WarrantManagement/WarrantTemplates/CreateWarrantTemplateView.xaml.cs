using Repairshop.Client.Common.Navigation;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;
/// <summary>
/// Interaction logic for CreateWarrantTemplateView.xaml
/// </summary>
public partial class CreateWarrantTemplateView 
    : ViewBase<CreateWarrantTemplateViewModel>
{
    public CreateWarrantTemplateView(CreateWarrantTemplateViewModel viewModel)
         : base(viewModel)
    {
        InitializeComponent();
    }
}
