using Ardalis.Specification;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;

internal class GetProcedureSummaryModelsSpecification
    : Specification<Procedure, ProcedureSummaryModel>
{
    public GetProcedureSummaryModelsSpecification()
    {
        Query
            .AsNoTracking();

        Query
            .Select(x => new ProcedureSummaryModel()
            {
                Id = x.Id,
                Name = x.Name,
                Color = x.Color
            });
    }
}
