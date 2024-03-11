using MediatR;
using Microsoft.Identity.Client.Extensions.Msal;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;
public class UpdateProcedureRequestHandler
    : IRequestHandler<UpdateProcedureRequest, UpdateProcedureResponse>
{
    private readonly IRepository<Procedure> _procedures;

    public UpdateProcedureRequestHandler(IRepository<Procedure> procedures)
    {
        _procedures = procedures;
    }

    public async Task<UpdateProcedureResponse> Handle(
        UpdateProcedureRequest request, 
        CancellationToken cancellationToken)
    {
        Procedure? procedure = await _procedures.GetByIdAsync(request.Id);

        if (procedure is null)
        {
            throw new EntityNotFoundException<Procedure, Guid>(request.Id);
        }

        procedure.Update(request.Name, ColorCode.FromHexCode(request.Color));

        await _procedures.UpdateAsync(procedure, cancellationToken);

        return new UpdateProcedureResponse();
    }
}
