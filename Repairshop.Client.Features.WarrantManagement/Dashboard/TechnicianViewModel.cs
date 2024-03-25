using CommunityToolkit.Mvvm.ComponentModel;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;
public class TechnicianViewModel
    : ObservableObject
{
    private IEnumerable<WarrantSummaryViewModel> _warrants = Enumerable.Empty<WarrantSummaryViewModel>();

    private TechnicianViewModel(
        string name,
        IEnumerable<WarrantSummaryViewModel> warrants)
    {
        Warrants = warrants;
        Name = name;
    }

    public IEnumerable<WarrantSummaryViewModel> Warrants { get => _warrants; set => SetProperty(ref _warrants, value); }
    public string Name { get; set; }

    public static TechnicianViewModel Create(
        string name,
        IEnumerable<WarrantSummaryViewModel> warrants) =>
        new TechnicianViewModel(name, warrants);

    public static TechnicianViewModel CreateUnassignedTechnician(IEnumerable<WarrantSummaryViewModel> warrants) =>
        new TechnicianViewModel("< Nedodjeljeno >", warrants);
}
