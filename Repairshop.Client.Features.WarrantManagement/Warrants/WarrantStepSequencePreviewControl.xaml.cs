using Repairshop.Client.Features.WarrantManagement.Procedures;
using System.Windows;
using System.Windows.Controls;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;
/// <summary>
/// Interaction logic for WarrantStepSequencePreviewControl.xaml
/// </summary>
public partial class WarrantStepSequencePreviewControl : UserControl
{
    public IEnumerable<Procedure> Procedures
    {
        get { return (IEnumerable<Procedure>)GetValue(ProceduresProperty); }
        set { SetValue(ProceduresProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ProceduresProperty =
        DependencyProperty.Register(nameof(Procedures), typeof(IEnumerable<Procedure>), typeof(WarrantStepSequencePreviewControl));

    public WarrantStepSequencePreviewControl()
    {
        InitializeComponent();
    }
}
