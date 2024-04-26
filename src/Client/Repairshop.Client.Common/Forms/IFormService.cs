namespace Repairshop.Client.Common.Forms;

public interface IFormService
{
    void ShowForm<TForm>() where TForm : FormBase;
}
