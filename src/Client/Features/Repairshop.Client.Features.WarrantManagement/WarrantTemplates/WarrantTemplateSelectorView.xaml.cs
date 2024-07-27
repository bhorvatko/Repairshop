using Repairshop.Client.Common.Interfaces;
using System.Windows.Controls;

namespace Repairshop.Client.Features.WarrantManagement.WarrantTemplates;
/// <summary>
/// Interaction logic for WarrantTemplateSelectorView.xaml
/// </summary>
public partial class WarrantTemplateSelectorView 
    : UserControl, IDialogContent
{
    public WarrantTemplateSelectorView(WarrantTemplateSelectorViewModel viewModel)
    {
        DataContext = viewModel;

        InitializeComponent();
    }
}
