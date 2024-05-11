namespace Repairshop.Client.Common.Forms;

public interface IFormService
{
    void ShowForm<TForm>() where TForm : FormBase;

    void ShowForm<TForm, TViewModel>(Action<TViewModel> viewModelConfig)
        where TForm : FormBase
        where TViewModel : IFormViewModel;
}
