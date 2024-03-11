using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class AdvanceWarrantRequest
    : IRequest<AdvanceWarrantResponse>
{
    public Guid WarrantId { get; set; }
    public Guid StepId { get; set; }
}
