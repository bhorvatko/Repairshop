using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Repairshop.Client.Features.WarrantManagement.Technicians;

public partial class EditTechnicianViewModel
    : ObservableValidator
{
    [ObservableProperty]
    [Required(ErrorMessage = "Unesite ime tehničara!")]
    public string _name = string.Empty;

    public bool Validate()
    {
        ValidateAllProperties();

        return !HasErrors;
    }
}
