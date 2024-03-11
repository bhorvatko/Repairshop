using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Technicians;
public class CreateTechnicianRequest
    : IRequest<CreateTechnicianResponse>
{
    public required string Name { get; set; }
}
