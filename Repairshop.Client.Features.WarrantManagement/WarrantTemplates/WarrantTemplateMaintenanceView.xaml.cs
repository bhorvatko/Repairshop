using Repairshop.Client.Common.Navigation;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;

/// <summary>
/// Interaction logic for WarrantTemplateMaintenanceView.xaml
/// </summary>
public partial class WarrantTemplateMaintenanceView 
    : ViewBase<WarrantTemplateMaintenanceViewModel>
{
    public WarrantTemplateMaintenanceView(WarrantTemplateMaintenanceViewModel viewModel) 
        : base(viewModel)
    {
        InitializeComponent();
    }
}
