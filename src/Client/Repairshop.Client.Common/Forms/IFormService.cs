namespace Repairshop.Client.Common.Forms;

public interface IFormService
{
    void ShowFormAsDialog<TForm>() where TForm : FormBase;

    void ShowFormAsDialog<TForm, TViewModel>(Action<TViewModel> viewModelConfig)
        where TForm : FormBase
        where TViewModel : IFormViewModel;

    void ShowForm<TForm>() where TForm : FormBase;
}
