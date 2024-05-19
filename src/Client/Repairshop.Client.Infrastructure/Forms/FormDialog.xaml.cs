using Repairshop.Client.Common.Forms;
using System.Windows;

namespace Repairshop.Client.Infrastructure.Forms;
/// <summary>
/// Interaction logic for FormContainer.xaml
/// </summary>
public partial class FormDialog 
    : Window
{
    public FormDialog()
    {
        InitializeComponent();
    }


    public void ShowWithContent(FormBase formContent)
    {
        FormContainer.FormContent = formContent;

        FormContainer.OnSubmissionFinished = Close;

        ShowDialog();
    }
}
