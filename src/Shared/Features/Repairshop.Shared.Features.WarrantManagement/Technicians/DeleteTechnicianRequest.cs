using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Technicians;

public class DeleteTechnicianRequest
    : IRequest<DeleteTechnicianResponse>
{
    public required Guid Id { get; set; }
}
