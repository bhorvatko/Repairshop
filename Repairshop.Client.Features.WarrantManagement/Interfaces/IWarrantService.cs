﻿using Repairshop.Client.Features.WarrantManagement.Dashboard;
using Repairshop.Client.Features.WarrantManagement.Warrants;

namespace Repairshop.Client.Features.WarrantManagement.Interfaces;

public interface IWarrantService
{
    Task CreateWarrant(
        string title,
        DateTime deadline,
        bool isUrgent,
        int number,
        IEnumerable<CreateWarrantStepDto> steps);

    Task UpdateWarrant(
        Guid id,
        string title,
        DateTime deadline,
        bool isUrgent,
        int number,
        IEnumerable<CreateWarrantStepDto> steps,
        Guid? currentStepProcedureId);

    Task<IEnumerable<WarrantSummaryViewModel>> GetUnassignedWarrants();
    Task<WarrantViewModel> GetWarrant(Guid id);
    Task AdvanceWarrant(Guid warrantId, Guid stepId);
    Task RollbackWarrant(Guid warrantId, Guid stepId);
    Task UnassignWarrant(Guid warrantId);
}