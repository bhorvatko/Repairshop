using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Common.Interfaces;

namespace Repairshop.Client.Common.Navigation;

public class ViewModelBase
    : ObservableObject, IViewModel
{
    public virtual void OnNavigatedAway() { }
    public virtual void Dispose() { }
}
