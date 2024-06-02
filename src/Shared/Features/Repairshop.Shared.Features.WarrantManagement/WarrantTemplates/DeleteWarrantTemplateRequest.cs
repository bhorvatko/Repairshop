using MediatR;

namespace Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;

public class DeleteWarrantTemplateRequest
    : IRequest<DeleteWarrantTemplateResponse>
{
    public required Guid Id { get; set; }
}
