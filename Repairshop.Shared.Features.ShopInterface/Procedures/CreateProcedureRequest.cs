using MediatR;

namespace Repairshop.Shared.Features.ShopInterface.Procedures;

public class CreateProcedureRequest
    : IRequest<CreateProcedureResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}
