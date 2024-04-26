namespace Repairshop.Client.Common.Forms;

public interface IFormViewModel
{
    Task SubmitForm();
    string GetSubmitText();
    bool ValidateForm();
}
