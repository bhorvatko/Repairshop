﻿using Ardalis.Specification;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;

public class GetProcedureModelsSpecification
    : Specification<Procedure, ProcedureModel>
{
    public GetProcedureModelsSpecification()
    {
        Query
            .AsNoTracking();

        Query
            .Select(x => new ProcedureModel()
            {
                Id = x.Id,
                Name = x.Name,
                Color = x.Color,
                UsedByWarrants = x
                    .WarrantSteps
                    .Select(x => x.Warrant.Title),
                UsedByWarrantTemplates = x
                    .WarrantTemplateSteps
                    .Select(x => x.WarrantTemplate.Name)
            });
    }
}
