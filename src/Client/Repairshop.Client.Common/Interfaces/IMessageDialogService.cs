namespace Repairshop.Client.Common.Interfaces;
public interface IMessageDialogService
{
    void ShowMessage(Exception exception);
    void ShowMessage(string title, string message);
    bool GetConfirmation(string message);
}
