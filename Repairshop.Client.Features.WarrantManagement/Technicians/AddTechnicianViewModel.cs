using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Common.Interfaces;
using Repairshop.Client.Features.WarrantManagement.Dashboard;

namespace Repairshop.Client.Features.WarrantManagement.Technicians;

public partial class AddTechnicianViewModel
    : IViewModel
{
    private readonly ITechnicianService _technicianService;
    private readonly ILoadingIndicatorService _loadingIndicatorService;

    public AddTechnicianViewModel(
        ITechnicianService technicianService, 
        ILoadingIndicatorService loadingIndicatorService)
    {
        _technicianService = technicianService;
        _loadingIndicatorService = loadingIndicatorService;
    }

    public string Name { get; set; } = string.Empty;

    [RelayCommand]
    public async Task CreateTechnician()
    {
        await _loadingIndicatorService.ShowLoadingIndicatorForAction(async () =>
        {
            await _technicianService.CreateTechnician(Name);
        });
    }
}
