using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;

internal class GetProcedureSummariesRequestHandler
    : IRequestHandler<GetProcedureSummariesRequest, GetProcedureSummariesResponse>
{
    private IRepository<Procedure> _procedures;

    public GetProcedureSummariesRequestHandler(IRepository<Procedure> procedures)
    {
        _procedures = procedures;
    }

    public async Task<GetProcedureSummariesResponse> Handle(
        GetProcedureSummariesRequest request, 
        CancellationToken cancellationToken)
    {
        GetProcedureSummaryModelsSpecification query = new();

        return new GetProcedureSummariesResponse()
        {
            Procedures = await _procedures.ListAsync(query, cancellationToken)
        };
    }
}
