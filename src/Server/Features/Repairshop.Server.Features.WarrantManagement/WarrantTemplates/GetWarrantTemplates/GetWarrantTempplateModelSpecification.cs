﻿using Ardalis.Specification;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Server.Features.WarrantManagement.WarrantTemplates.GetWarrantTemplates;

internal class GetWarrantTempplateModelSpecification
    : Specification<WarrantTemplate, WarrantTemplateModel>
{
    public GetWarrantTempplateModelSpecification()
    {
        Query
            .Select(x => new WarrantTemplateModel()
            {
                Id = x.Id,
                Name = x.Name,
                Steps = x.Steps.Select(s => new WarrantStepModel()
                {
                    CanBeTransitionedToByFrontOffice = s.CanBeTransitionedToByFrontOffice,
                    CanBeTransitionedToByWorkshop = s.CanBeTransitionedToByWorkshop,
                    Procedure = new ProcedureSummaryModel()
                    {
                        Id = s.Procedure.Id,
                        Name = s.Procedure.Name,
                        Color = s.Procedure.Color,
                        Priority = s.Procedure.Priority
                    }
                })
            });
    }
}