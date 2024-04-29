using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Procedures;

public class DeleteProcedureRequest
    : IRequest<DeleteProcedureResponse>
{
    public required Guid Id { get; set; }
}
