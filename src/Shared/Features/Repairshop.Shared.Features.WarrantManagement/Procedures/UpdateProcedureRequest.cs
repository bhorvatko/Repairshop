using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Procedures;

public class UpdateProcedureRequest
    : IRequest<UpdateProcedureResponse>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }   
    public required string Color { get; set; }
}
