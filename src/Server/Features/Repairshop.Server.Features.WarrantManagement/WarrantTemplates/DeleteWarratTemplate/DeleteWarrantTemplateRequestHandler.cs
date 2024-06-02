using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.WarrantTemplates;

namespace Repairshop.Server.Features.WarrantManagement.WarrantTemplates.DeleteWarratTemplate;

internal class DeleteWarrantTemplateRequestHandler
    : IRequestHandler<DeleteWarrantTemplateRequest, DeleteWarrantTemplateResponse>
{
    private readonly IRepository<WarrantTemplate> _warrantTemplates;

    public DeleteWarrantTemplateRequestHandler(IRepository<WarrantTemplate> warrantTemplates)
    {
        _warrantTemplates = warrantTemplates;
    }

    public async Task<DeleteWarrantTemplateResponse> Handle(
        DeleteWarrantTemplateRequest request,
        CancellationToken cancellationToken)
    {
        WarrantTemplate? warrantTemplate =
            await _warrantTemplates.GetByIdAsync(request.Id, cancellationToken);

        if (warrantTemplate is null)
        {
            throw new EntityNotFoundException<WarrantTemplate, Guid>(request.Id);
        }

        await _warrantTemplates.DeleteAsync(warrantTemplate, cancellationToken);

        return new DeleteWarrantTemplateResponse();
    }
}
