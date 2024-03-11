using CommunityToolkit.Mvvm.ComponentModel;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;
public class TechnicianViewModel
    : ObservableObject
{
    private IEnumerable<WarrantViewModel> _warrants = Enumerable.Empty<WarrantViewModel>();

    private TechnicianViewModel(
        string name,
        IEnumerable<WarrantViewModel> warrants)
    {
        Warrants = warrants;
        Name = name;
    }

    public IEnumerable<WarrantViewModel> Warrants { get => _warrants; set => SetProperty(ref _warrants, value); }
    public string Name { get; set; }

    public static TechnicianViewModel Create(
        string name,
        IEnumerable<WarrantViewModel> warrants) =>
        new TechnicianViewModel(name, warrants);

}
