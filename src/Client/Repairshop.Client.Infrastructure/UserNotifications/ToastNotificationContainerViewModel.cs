using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Media;

namespace Repairshop.Client.Infrastructure.UserNotifications;

public partial class ToastNotificationContainerViewModel
    : ObservableObject
{
    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty]
    private SolidColorBrush _backgroundBrush = new SolidColorBrush(Colors.Transparent);

    [ObservableProperty]
    private Visibility _visibility  = Visibility.Collapsed;

    public Task ShowMessage(
        string message,
        System.Drawing.Color color)
    {
        Message = message;
        BackgroundBrush = new SolidColorBrush(ConvertColor(color));
        Visibility = Visibility.Visible;

         Task.Delay(5000).ContinueWith(_ =>
         {
             Visibility = Visibility.Collapsed;
         });

        return Task.CompletedTask;
    }

    [RelayCommand]
    public void OnCloseButtonClicked()
    {
        Visibility = Visibility.Collapsed;
    }

    private static Color ConvertColor(System.Drawing.Color color)
    {
        return Color.FromArgb(color.A, color.R, color.G, color.B);
    }
}
