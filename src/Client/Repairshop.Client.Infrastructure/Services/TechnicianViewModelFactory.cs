using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Shared.Features.WarrantManagement.Technicians;

namespace Repairshop.Client.Infrastructure.Services;

internal class TechnicianViewModelFactory
{
    private readonly WarrantSummaryViewModelFactory _warrantSummaryViewModelFactory;

    public TechnicianViewModelFactory(WarrantSummaryViewModelFactory warrantSummaryViewModelFactory)
    {
        _warrantSummaryViewModelFactory = warrantSummaryViewModelFactory;
    }

    public TechnicianViewModel Create(TechnicianModel model) =>
        TechnicianViewModel.Create(
            model.Id,
            model.Name,
            model.Warrants.Select(_warrantSummaryViewModelFactory.Create));
}
