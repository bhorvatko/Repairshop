using Repairshop.Server.Common.ClientContext;
using Repairshop.Shared.Common.ClientContext;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.GetWarrants;

internal class WarrantModelFactory
{
    private readonly IClientContextProvider _clientContextProvider;

    public WarrantModelFactory(IClientContextProvider clientContextProvider)
    {
        _clientContextProvider = clientContextProvider;
    }

    public IEnumerable<WarrantModel> Create(IEnumerable<WarrantQueryModel> queryModels)
    {
        return queryModels.Select(x => new WarrantModel()
        {
            Id = x.Id,
            Deadline = x.Deadline,
            IsUrgent = x.IsUrgent,
            Title = x.Title,
            TechnicianId = x.TechnicianId,
            Procedure = x.Procedure,
            CanBeAdvanced = CanBeTransitioned(x.CanBeAdvancedByFrontOffice, x.CanBeAdvancedByWorkshop),
            CanBeRolledBack = CanBeTransitioned(x.CanBeRolledBackByFrontOffice, x.CanBeRolledBakByWorkshop),
            NextStepId = x.NextStepId,
            PreviousStepId = x.PreviousStepId,
        });
    }

    private bool CanBeTransitioned(
        bool canBeTransitionedByFrontOffice,
        bool canBeTransitionedByWorkshop) =>
        canBeTransitionedByFrontOffice && ClientIsFrontOffice()
            || canBeTransitionedByWorkshop && ClientIsWorkshop();

    private bool ClientIsFrontOffice() =>
        GetClientContext() == RepairshopClientContext.FrontOffice;

    private bool ClientIsWorkshop() =>
        GetClientContext() == RepairshopClientContext.Workshop;

    private string GetClientContext() =>
        _clientContextProvider.GetClientContext();
}
