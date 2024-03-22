using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class GetWarrantsRequest
    : IRequest<GetWarrantsResponse>
{
    public Guid? TechnicianId { get; set; }
}
