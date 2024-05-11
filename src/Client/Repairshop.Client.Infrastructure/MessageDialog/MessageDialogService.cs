using Repairshop.Client.Common.Interfaces;
using System.Windows;

namespace Repairshop.Client.Infrastructure.MessageDialog;

public class MessageDialogService
    : IMessageDialogService
{
    public void ShowMessage(Exception exception) =>
        MessageBox.Show(
            messageBoxText: exception.Message,
            caption: "Unexpected error",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Error);

    public void ShowMessage(string title, string message) =>
        MessageBox.Show(
            messageBoxText: message,
            caption: title,
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Information);

    public bool GetConfirmation(string message) =>
        MessageBox.Show(
            messageBoxText: message,
            caption: "Potvrda",
            button: MessageBoxButton.YesNo,
            icon: MessageBoxImage.Question) 
            == MessageBoxResult.Yes;
}
