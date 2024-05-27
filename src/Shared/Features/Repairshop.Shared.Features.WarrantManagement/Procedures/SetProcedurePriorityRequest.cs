using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Procedures;

public class SetProcedurePriorityRequest
    : IRequest<SetProcedurePriorityResponse>
{
    public required Guid ProcedureId { get; set; }
    public required float Priority { get; set; }
}
