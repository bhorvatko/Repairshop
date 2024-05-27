using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Procedures;

public class CreateProcedureRequest
    : IRequest<CreateProcedureResponse>
{
    public required string Name { get; set; }
    public required string Color { get; set; }
    public required float Priority { get; set; }
}
