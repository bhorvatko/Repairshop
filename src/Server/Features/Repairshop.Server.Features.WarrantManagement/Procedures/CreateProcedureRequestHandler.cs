using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Procedures;

namespace Repairshop.Server.Features.WarrantManagement.Procedures;
internal class CreateProcedureRequestHandler
    : IRequestHandler<CreateProcedureRequest, CreateProcedureResponse>
{
    private readonly IRepository<Procedure> _procedures;

    public CreateProcedureRequestHandler(IRepository<Procedure> procedures)
    {
        _procedures = procedures;
    }

    public async Task<CreateProcedureResponse> Handle(
        CreateProcedureRequest request,
        CancellationToken cancellationToken)
    {
        Procedure procedure = Procedure.Create(
            request.Name, 
            ColorCode.FromHexCode(request.Color),
            ProcedurePriority.FromFloating(request.Priority));

        await _procedures.AddAsync(procedure, cancellationToken);

        return new CreateProcedureResponse()
        {
            Id = procedure.Id
        };
    }
}