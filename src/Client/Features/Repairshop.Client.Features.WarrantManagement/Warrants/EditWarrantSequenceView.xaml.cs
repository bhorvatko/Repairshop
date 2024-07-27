using Repairshop.Client.Common.Interfaces;
using System.Windows.Controls;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

/// <summary>
/// Interaction logic for EditWarrantSequenceView.xaml
/// </summary>
public partial class EditWarrantSequenceView 
    : UserControl, IDialogContent
{
    public EditWarrantSequenceView(EditWarrantSequenceViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();
    }
}
