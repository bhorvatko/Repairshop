using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;

internal class SetProcedurePriorityRequestHandler
    : IRequestHandler<SetProcedurePriorityRequest, SetProcedurePriorityResponse>
{
    private readonly IRepository<Procedure> _procedures;

    public SetProcedurePriorityRequestHandler(IRepository<Procedure> procedures)
    {
        _procedures = procedures;
    }

    public async Task<SetProcedurePriorityResponse> Handle(
        SetProcedurePriorityRequest request,
        CancellationToken cancellationToken)
    {
        Procedure? procedure = 
            await _procedures.GetByIdAsync(request.ProcedureId, cancellationToken);

        if (procedure is null)
        {
            throw new EntityNotFoundException<Procedure, Guid>(request.ProcedureId);
        }

        procedure.SetPriority(ProcedurePriority.FromFloating(request.Priority));

        await _procedures.SaveChangesAsync(cancellationToken);

        return new SetProcedurePriorityResponse();
    }
}
