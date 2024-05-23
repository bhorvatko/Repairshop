using Microsoft.Extensions.DependencyInjection;
using Repairshop.Client.Common.Forms;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Infrastructure.Navigation;

namespace Repairshop.Client.Infrastructure.Forms;

internal class FormService
    : IFormService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly INavigationService _navigationService;
    private readonly DialogHostManager _dialogHostManager;

    public FormService(
        IServiceProvider serviceProvider,
        INavigationService navigationService,
        DialogHostManager dialogHostManager)
    {
        _serviceProvider = serviceProvider;
        _navigationService = navigationService;
        _dialogHostManager = dialogHostManager;
    }

    public void ShowForm<TForm>() 
        where TForm : FormBase
    {
        FormBase form =
            _serviceProvider.GetRequiredService<TForm>();

        FormView formView = new FormView(form, () => { });

        _navigationService.NavigateToView<FormView>(formView);
    }

    public Task ShowFormAsDialog<TForm>()
        where TForm : FormBase =>
        ShowFormAsDialogInternal<TForm>();

    public Task ShowFormAsDialog<TForm, TViewModel>(Action<TViewModel> viewModelConfig)
        where TForm : FormBase
        where TViewModel : IFormViewModel =>
        ShowFormAsDialogInternal<TForm>(viewModel => viewModelConfig((TViewModel)viewModel));

    private async Task ShowFormAsDialogInternal<TForm>(Action<object>? viewModelConfig = null)
        where TForm : FormBase
    {
        FormBase form =
            _serviceProvider.GetRequiredService<TForm>();

        viewModelConfig?.Invoke(form.DataContext);

        TaskCompletionSource taskCompletionSource = new TaskCompletionSource();

        FormContainer formContainer =
            new FormContainer(form, () => taskCompletionSource.TrySetResult());

        _dialogHostManager.Show(formContainer);

        await taskCompletionSource.Task;

        _dialogHostManager.Close();
    }
}
