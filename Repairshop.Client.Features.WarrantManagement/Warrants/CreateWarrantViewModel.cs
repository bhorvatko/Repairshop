using Repairshop.Client.Common.Interfaces;

namespace Repairshop.Client.Features.WarrantManagement.Warrants;

public class CreateWarrantViewModel
    : IViewModel
{
    public CreateWarrantViewModel(EditWarrantViewModel editWarrantViewModel)
    {
        EditWarrantViewModel = editWarrantViewModel;
    }

    public EditWarrantViewModel EditWarrantViewModel { get; private set; }
}
