using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Navigation;
using System.Windows.Controls;

namespace Repairshop.Client.Infrastructure.Forms;

/// <summary>
/// Interaction logic for FormView.xaml
/// </summary>
public partial class FormView : UserControl, IViewBase
{
    public FormView(FormBase formContent, Action onSubmissionFinished)
    {
        InitializeComponent();

        FormContainer.FormContent = formContent;

        FormContainer.OnSubmissionFinished = onSubmissionFinished;
    }
}
