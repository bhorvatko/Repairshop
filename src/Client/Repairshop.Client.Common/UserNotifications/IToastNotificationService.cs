namespace Repairshop.Client.Common.UserNotifications;

public interface IToastNotificationService
{
    Task ShowSuccess(string message);
}
