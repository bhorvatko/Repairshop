using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Forms;

namespace Repairshop.Client.Infrastructure.Forms;

internal class FormService
    : IFormService
{
    private readonly IServiceProvider _serviceProvider;

    public FormService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void ShowForm<TForm>() where TForm : FormBase
    {
        throw new NotImplementedException();
    }

    public void ShowFormAsDialog<TForm>()
        where TForm : FormBase
    {
        FormDialog formContainer = new FormDialog();

        FormBase form =
            _serviceProvider.GetRequiredService<TForm>();

        formContainer.ShowWithContent(form);
    }

    public void ShowFormAsDialog<TForm, TViewModel>(Action<TViewModel> viewModelConfig)
        where TForm : FormBase
        where TViewModel : IFormViewModel
    {
        FormDialog formContainer = new FormDialog();

        FormBase form =
            _serviceProvider.GetRequiredService<TForm>();

        viewModelConfig((TViewModel)form.DataContext);

        formContainer.ShowWithContent(form);
    }
}
