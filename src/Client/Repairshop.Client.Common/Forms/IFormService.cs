namespace Repairshop.Client.Common.Forms;

public interface IFormService
{
    Task ShowFormAsDialog<TForm>() where TForm : FormBase;

    Task ShowFormAsDialog<TForm, TViewModel>(Action<TViewModel> viewModelConfig)
        where TForm : FormBase
        where TViewModel : IFormViewModel;

    void ShowForm<TForm>() where TForm : FormBase;
}
