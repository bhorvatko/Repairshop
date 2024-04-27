using Repairshop.Client.Common.UserNotifications;
using System.Drawing;

namespace Repairshop.Client.Infrastructure.UserNotifications;

public class ToastNotificationService
    : IToastNotificationService
{
    private readonly ToastNotificationContainerViewModel _toastNotificationContainerViewModel;

    public ToastNotificationService(ToastNotificationContainerViewModel toastNotificationContainerViewModel)
    {
        _toastNotificationContainerViewModel = toastNotificationContainerViewModel;
    }

    public async Task ShowSuccess(string message)
    {
        await _toastNotificationContainerViewModel.ShowMessage(
            message,
            Color.LightGreen);
    }
}
