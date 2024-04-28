using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Common.Interfaces;
using System.Windows;

namespace Repairshop.Client.Infrastructure.Bootstrapping;

public abstract partial class MainViewModelBase
    : ObservableObject, IMainViewModel
{
    private Visibility _loadingIndicatorVisibility = Visibility.Visible;

    public virtual Visibility LoadingIndicatorVisibility 
    { 
        get => _loadingIndicatorVisibility; 
        set => SetProperty(ref _loadingIndicatorVisibility, value); 
    }

    public virtual void ShowLoadingIndicator()
    {
        LoadingIndicatorVisibility = Visibility.Visible;
    }

    public virtual void HideLoadingIndicator()
    {
        LoadingIndicatorVisibility = Visibility.Collapsed;
    }
}
