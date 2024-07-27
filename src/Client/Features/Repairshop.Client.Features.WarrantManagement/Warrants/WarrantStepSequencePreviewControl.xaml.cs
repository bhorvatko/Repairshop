using Repairshop.Client.Features.WarrantManagement.Procedures;
using System.Windows;
using System.Windows.Controls;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;
/// <summary>
/// Interaction logic for WarrantStepSequencePreviewControl.xaml
/// </summary>
public partial class WarrantStepSequencePreviewControl : UserControl
{
    public IEnumerable<ProcedureSummaryViewModel> Procedures
    {
        get { return (IEnumerable<ProcedureSummaryViewModel>)GetValue(ProceduresProperty); }
        set { SetValue(ProceduresProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ProceduresProperty =
        DependencyProperty.Register(nameof(Procedures), typeof(IEnumerable<ProcedureSummaryViewModel>), typeof(WarrantStepSequencePreviewControl));

    public WarrantStepSequencePreviewControl()
    {
        InitializeComponent();
    }
}
