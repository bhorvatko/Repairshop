using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.Warrants;

public class UpdateWarrantRequest
    : IRequest<UpdateWarrantResponse>
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required DateTime Deadline { get; set; }
    public required bool IsUrgent { get; set; }
    public required int Number { get; set; }
    public required IEnumerable<WarrantStepDto> Steps { get; set; }
    public Guid? CurrentStepProcedureId { get; set; }
}
