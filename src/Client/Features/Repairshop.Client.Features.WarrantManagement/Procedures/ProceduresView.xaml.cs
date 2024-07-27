using Repairshop.Client.Common.Navigation;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;
/// <summary>
/// Interaction logic for ProceduresView.xaml
/// </summary>
public partial class ProceduresView 
    : ViewBase<ProceduresViewModel>
{
    public ProceduresView(ProceduresViewModel viewModel) 
        : base(viewModel)
    {
        InitializeComponent();
    }
}
