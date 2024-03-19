using Repairshop.Client.Common.Navigation;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;
/// <summary>
/// Interaction logic for CreateWarrantView.xaml
/// </summary>
public partial class CreateWarrantView
    : ViewBase<CreateWarrantViewModel>
{
    public CreateWarrantView(CreateWarrantViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
