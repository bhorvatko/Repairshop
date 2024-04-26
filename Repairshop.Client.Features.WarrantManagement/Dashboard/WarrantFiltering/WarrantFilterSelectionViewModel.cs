using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard.WarrantFiltering;

public partial class WarrantFilterSelectionViewModel
    : ObservableObject
{
    private readonly IProcedureService _procedureService;
    private readonly IReadOnlyCollection<Guid> _initialFilteredProcedureIds;

    [ObservableProperty]
    private IReadOnlyCollection<ProcedureFilterViewModel>? _procedures;

    public WarrantFilterSelectionViewModel(
        IProcedureService procedureService,
        IEnumerable<Guid> filteredProcedureIds)
    {
        _procedureService = procedureService;
        _initialFilteredProcedureIds = filteredProcedureIds.ToList();
    }

    public IReadOnlyCollection<Guid> FilteredProcedureIds =>
        Procedures?
            .Where(x => !x.Selected)
            .Select(x => x.Procedure.Id!.Value)
            .ToList()
            ?? _initialFilteredProcedureIds;

    [RelayCommand]
    public async Task OnLoaded()
    {
        if (Procedures is not null) return;

        Procedures = (await _procedureService.GetProcedures())
            .Select(CreateProcedureFilterViewModel)
            .ToList();
    }

    private ProcedureFilterViewModel CreateProcedureFilterViewModel(Procedure procedure) =>
        new ProcedureFilterViewModel(
            procedure,
            !_initialFilteredProcedureIds.Any(x => x == procedure.Id));
}
