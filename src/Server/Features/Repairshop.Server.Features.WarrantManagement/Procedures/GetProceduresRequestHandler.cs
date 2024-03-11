using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;

internal class GetProceduresRequestHandler
    : IRequestHandler<GetProceduresRequest, GetProceduresResponse>
{
    private IRepository<Procedure> _procedures;

    public GetProceduresRequestHandler(IRepository<Procedure> procedures)
    {
        _procedures = procedures;
    }

    public async Task<GetProceduresResponse> Handle(
        GetProceduresRequest request, 
        CancellationToken cancellationToken)
    {
        GetProcedureModelsSpecification query = new();

        return new GetProceduresResponse()
        {
            Procedures = await _procedures.ListAsync(query, cancellationToken)
        };
    }
}
