using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class RollbackWarrantRequest
    : IRequest<RollbackWarrantResponse>
{
    public Guid WarrantId { get; set; }
    public Guid StepId { get; set; }
}
