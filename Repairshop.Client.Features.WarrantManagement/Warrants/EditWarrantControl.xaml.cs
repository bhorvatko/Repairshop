using System.Windows;
using System.Windows.Controls;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;
/// <summary>
/// Interaction logic for EditWarrantControl.xaml
/// </summary>
public partial class EditWarrantControl : UserControl
{
    public EditWarrantViewModel ViewModel
    {
        get { return (EditWarrantViewModel)GetValue(ViewModelProperty); }
        set { SetValue(ViewModelProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(nameof(ViewModel), typeof(EditWarrantViewModel), typeof(EditWarrantControl));

    public EditWarrantControl()
    {
        InitializeComponent();
    }
}
