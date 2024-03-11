using System.Windows;

namespace Repairshop.Client.Infrastructure.MessageDialog;

public class MessageDialogService
{
    public void ShowMessage(Exception exception) =>
        MessageBox.Show(
            messageBoxText: exception.Message,
            caption: "Unexpected error",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Error);
}
