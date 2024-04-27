using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Common.Interfaces;
using System.Windows;

namespace Repairshop.Client.Infrastructure.Bootstrapping;

public abstract partial class MainViewModelBase
    : ObservableObject, IMainViewModel
{
    [ObservableProperty]
    private Visibility _loadingIndicatorVisibility = Visibility.Visible;

    public void ShowLoadingIndicator()
    {
        LoadingIndicatorVisibility = Visibility.Visible;
    }

    public void HideLoadingIndicator()
    {
        LoadingIndicatorVisibility = Visibility.Collapsed;
    }
}
