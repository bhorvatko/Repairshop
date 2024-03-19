using System.Windows.Controls;

namespace Repairshop.Client.Common.Navigation;

public class ViewBase<TViewModel>
    : UserControl, IViewBase
{
    public ViewBase(TViewModel viewModel)
    {
        DataContext = viewModel;
    }
}

public interface IViewBase
{
}
