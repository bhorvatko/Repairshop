using MaterialDesignThemes.Wpf;
using System.Windows.Controls;

namespace Repairshop.Client.Infrastructure.Navigation;

internal class DialogHostManager
{
    private readonly Stack<UserControl> _dialogHostContent = 
        new Stack<UserControl>();

    public void Show(UserControl userControl)
    {
        _dialogHostContent.Push(userControl);

        UpdateDialogContent();
    }

    public void Close()
    {
        if (_dialogHostContent.Count > 0)
        {
            _dialogHostContent.Pop();
        }

        UpdateDialogContent();
    }

    private void UpdateDialogContent()
    {
        UserControl? newContent = 
            _dialogHostContent.FirstOrDefault();

        if (newContent is null)
        {
            DialogHost.Close(null);
            return;
        }

        if (!DialogHost.IsDialogOpen(null))
        {
            _ = DialogHost.Show(newContent);
        }
        else
        {
            DialogHost.GetDialogSession(null)?.UpdateContent(newContent);
        }
    }
}
