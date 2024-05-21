using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;

namespace Repairshop.Client.Infrastructure.Forms;

internal class FormService
    : IFormService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly INavigationService _navigationService;

    public FormService(
        IServiceProvider serviceProvider,
        INavigationService navigationService)
    {
        _serviceProvider = serviceProvider;
        _navigationService = navigationService;
    }

    public void ShowForm<TForm>() 
        where TForm : FormBase
    {
        FormBase form =
            _serviceProvider.GetRequiredService<TForm>();

        FormView formView = new FormView(form, () => { });

        _navigationService.NavigateToView<FormView>(formView);
    }

    public async Task ShowFormAsDialog<TForm>()
        where TForm : FormBase
    {
        FormBase form =
            _serviceProvider.GetRequiredService<TForm>();

        FormContainer formContainer =
            new FormContainer(form, () => DialogHost.Close(null));

        await DialogHost.Show(formContainer);
    }

    public async Task ShowFormAsDialog<TForm, TViewModel>(Action<TViewModel> viewModelConfig)
        where TForm : FormBase
        where TViewModel : IFormViewModel
    {

        FormBase form =
            _serviceProvider.GetRequiredService<TForm>();

        viewModelConfig((TViewModel)form.DataContext);

        FormContainer formContainer = 
            new FormContainer(form, () => DialogHost.Close(null));

        await DialogHost.Show(formContainer);
    }
}
