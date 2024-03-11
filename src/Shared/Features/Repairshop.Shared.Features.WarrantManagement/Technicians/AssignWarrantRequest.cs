using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Technicians;

public class AssignWarrantRequest
    : IRequest<AssignWArrantResponse>
{
    public required Guid WarrantId { get; set; }
    public required Guid TechnicianId { get; set; }
}
