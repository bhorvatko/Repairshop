﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard;
public partial class TechnicianViewModel
    : ObservableObject, IEquatable<TechnicianViewModel>
{
    [ObservableProperty]
    private IEnumerable<WarrantSummaryViewModel> _warrants = Enumerable.Empty<WarrantSummaryViewModel>();

    private TechnicianViewModel(
        Guid? id,
        string name,
        IEnumerable<WarrantSummaryViewModel> warrants)
    {
        Id = id;
        Warrants = warrants;
        Name = name;
    }

    public Guid? Id { get; private set; }
    public string Name { get; set; }

    public static TechnicianViewModel Create(
        Guid id,
        string name,
        IEnumerable<WarrantSummaryViewModel> warrants) =>
        new TechnicianViewModel(id, name, warrants);

    public static TechnicianViewModel CreateUnassignedTechnician(IEnumerable<WarrantSummaryViewModel> warrants) =>
        new TechnicianViewModel(null, "< Nedodjeljeno >", warrants);

    public bool Equals(TechnicianViewModel? other)
    {
        if (other is null) return false;

        return Id == other.Id;
    }
}