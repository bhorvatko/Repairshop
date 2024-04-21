﻿using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Infrastructure.ClientContext;
using Repairshop.Shared.Common.ClientContext;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Infrastructure.Services;

internal class WarrantSummaryViewModelFactory
{
    private readonly ClientContextProvider _clientContextProvider;

    public WarrantSummaryViewModelFactory(ClientContextProvider clientContextProvider)
    {
        _clientContextProvider = clientContextProvider;
    }

    public WarrantSummaryViewModel Create(WarrantModel model) =>
        WarrantSummaryViewModel.Create(
            model.Id,
            model.IsUrgent,
            model.Deadline,
            model.Procedure.ToViewModel(),
            model.Title,
            CanBeTransitioned(model.CanBeRolledBackByFrontOffice, model.CanBeRolledBackByWorkshop),
            CanBeTransitioned(model.CanBeAdvancedByFrontOffice, model.CanBeAdvancedByWorkshop),
            model.NextStepId,
            model.PreviousStepId);

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
        _clientContextProvider.ClientContext;
}