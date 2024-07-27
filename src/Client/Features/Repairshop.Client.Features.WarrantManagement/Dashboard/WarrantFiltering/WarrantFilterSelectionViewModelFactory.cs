using Repairshop.Client.Features.WarrantManagement.Procedures;

namespace Repairshop.Client.Features.WarrantManagement.Dashboard.WarrantFiltering;

public class WarrantFilterSelectionViewModelFactory
{
    private readonly IProcedureService _procedureService;

    public WarrantFilterSelectionViewModelFactory(IProcedureService procedureService)
    {
        _procedureService = procedureService;
    }

    public WarrantFilterSelectionViewModel Create(
        IEnumerable<Guid> filteredProcedureIds) =>
        new WarrantFilterSelectionViewModel(
            _procedureService,
            filteredProcedureIds);
}
