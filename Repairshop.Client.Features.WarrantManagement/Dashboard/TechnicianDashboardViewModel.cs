using CommunityToolkit.Mvvm.ComponentModel;
using Repairshop.Client.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public partial class TechnicianDashboardViewModel
    : ObservableObject
{
    private readonly WarrantPreviewControlViewModelFactory _warrantPreviewControlViewModelFactory;

    [ObservableProperty]
    private IEnumerable<TechnicianViewModel> _availableTechnicians = 
        Enumerable.Empty<TechnicianViewModel>();

    [ObservableProperty]
    private IEnumerable<WarrantPreviewControlViewModel> _warrants = 
        Enumerable.Empty<WarrantPreviewControlViewModel>();

    private TechnicianViewModel? _selectedTechnician = null;

    public TechnicianDashboardViewModel(
        WarrantPreviewControlViewModelFactory warrantPreviewControlViewModelFactory,
        IReadOnlyCollection<TechnicianViewModel> availableTechnicians,
        Guid? selectedTechnicianId)
    {
        _warrantPreviewControlViewModelFactory = warrantPreviewControlViewModelFactory;

        AvailableTechnicians = availableTechnicians;

        SelectedTechnician =
            availableTechnicians.FirstOrDefault(x => x.Id == selectedTechnicianId);
    }

    public TechnicianViewModel? SelectedTechnician
    {
        get => _selectedTechnician;
        set
        {
            SetProperty(ref _selectedTechnician, value);

            Warrants = value?
                .Warrants
                .Select(x => 
                    _warrantPreviewControlViewModelFactory.CreateViewModel(x))
                ?? Enumerable.Empty<WarrantPreviewControlViewModel>();
        }
    }
}
