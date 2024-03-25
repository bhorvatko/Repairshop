using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class GetWarrantRequest
    : IRequest<GetWarrantResponse>
{
    public required Guid Id { get; set; }
}
