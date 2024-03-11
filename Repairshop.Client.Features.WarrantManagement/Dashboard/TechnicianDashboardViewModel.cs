using CommunityToolkit.Mvvm.ComponentModel;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;

public class TechnicianDashboardViewModel
    : ObservableObject
{
    private IEnumerable<TechnicianViewModel> _availableTechnicians = Enumerable.Empty<TechnicianViewModel>();
    private TechnicianViewModel? _selectedTechnician = null;
    private IEnumerable<WarrantPreviewControlViewModel> _warrants = Enumerable.Empty<WarrantPreviewControlViewModel>();

    public TechnicianDashboardViewModel(IEnumerable<TechnicianViewModel> availableTechnicians)
    {
        AvailableTechnicians = availableTechnicians;
    }

    public IEnumerable<TechnicianViewModel> AvailableTechnicians { get => _availableTechnicians; set => SetProperty(ref _availableTechnicians, value); }
    public IEnumerable<WarrantPreviewControlViewModel> Warrants { get => _warrants; set => SetProperty(ref _warrants, value); }

    public TechnicianViewModel? SelectedTechnician
    {
        get => _selectedTechnician;
        set
        {
            SetProperty(ref _selectedTechnician, value);

            Warrants = value?.Warrants.Select(x => new WarrantPreviewControlViewModel(x))
                ?? Enumerable.Empty<WarrantPreviewControlViewModel>();
        }
    }
}
