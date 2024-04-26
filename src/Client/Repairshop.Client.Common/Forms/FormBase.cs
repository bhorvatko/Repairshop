using System.Windows.Controls;

namespace Repairshop.Client.Common.Forms;

public abstract class FormBase
    : UserControl
{
    public FormBase(IFormViewModel viewModel)
    {
        DataContext = viewModel;
    }

    public IFormViewModel ViewModel => (IFormViewModel)DataContext;
}
