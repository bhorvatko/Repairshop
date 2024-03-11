using System.Windows;
using System.Windows.Controls;

namespace Repairshop.Client.Features.WarrantManagement.Procedures;
/// <summary>
/// Interaction logic for ProcedurePreview.xaml
/// </summary>
public partial class ProcedurePreview 
    : UserControl
{
    public Procedure Procedure
    {
        get => (Procedure)GetValue(ProcedureProperty);
        set
        {
            SetValue(ProcedureProperty, value);
        }
    }

    public static readonly DependencyProperty ProcedureProperty = 
        DependencyProperty.Register(nameof(Procedure), typeof(Procedure), typeof(ProcedurePreview));

    public ProcedurePreview()
    {
        InitializeComponent();
    }
}
