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

    public void ShowForm<TForm>()
        where TForm : FormBase
    {
        FormContainer formContainer = new FormContainer();

        FormBase form =
            _serviceProvider.GetRequiredService<TForm>();

        formContainer.ShowWithContent(form);
    }
}
