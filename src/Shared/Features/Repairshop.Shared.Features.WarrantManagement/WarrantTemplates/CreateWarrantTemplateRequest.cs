using MediatR;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;

public class CreateWarrantTemplateRequest
    : IRequest<CreateWarrantTemplateResponse>
{
    public required string Name { get; set; }
    public required IReadOnlyCollection<WarrantStepDto> Steps { get; set; }
}
