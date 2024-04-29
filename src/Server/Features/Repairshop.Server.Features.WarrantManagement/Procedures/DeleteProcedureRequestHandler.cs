using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;

internal class DeleteProcedureRequestHandler
    : IRequestHandler<DeleteProcedureRequest, DeleteProcedureResponse>
{
    private readonly IRepository<Procedure> _procedures;

    public DeleteProcedureRequestHandler(IRepository<Procedure> procedures)
    {
        _procedures = procedures;
    }

    public async Task<DeleteProcedureResponse> Handle(
        DeleteProcedureRequest request, 
        CancellationToken cancellationToken)
    {
        GetProceduresSpecification specification = new(new[] { request.Id });

        Procedure? procedure = 
            await _procedures.FirstOrDefaultAsync(specification, cancellationToken);

        if (procedure is null)
        {
            throw new EntityNotFoundException<Procedure, Guid>(request.Id);
        }

        await _procedures.DeleteAsync(procedure, cancellationToken);

        return new DeleteProcedureResponse();
    }
}
