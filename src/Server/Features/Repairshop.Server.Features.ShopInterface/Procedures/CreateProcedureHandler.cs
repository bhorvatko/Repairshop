using MediatR;
using Repairshop.Shared.Features.ShopInterface.Procedures;

namespace Repairshop.Server.Features.ShopInterface.Procedures;
internal class CreateProcedureHandler
    : IRequestHandler<CreateProcedureRequest, CreateProcedureResponse>
{
    public Task<CreateProcedureResponse> Handle(
        CreateProcedureRequest request, 
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new CreateProcedureResponse());
    }
}
