using Repairshop.Client.Common.Navigation;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;
/// <summary>
/// Interaction logic for UpdateWarrantView.xaml
/// </summary>
public partial class UpdateWarrantView
    : ViewBase<UpdateWarrantViewModel>
{
    public UpdateWarrantView(UpdateWarrantViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
