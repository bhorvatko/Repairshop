using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Technicians;

public class UpdateTechnicianRequest
    : IRequest<UpdateTechnicianResponse>
{
    public required Guid TechnicianId { get; set; }
    public required string Name { get; set; }
}
