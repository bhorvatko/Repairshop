using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class UnassignWarrantRequest
    : IRequest<UnassignWarrantResponse>
{
    public required Guid Id { get; set; }
}
