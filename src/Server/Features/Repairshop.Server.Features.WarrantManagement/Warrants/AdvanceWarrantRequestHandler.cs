using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants;

internal class AdvanceWarrantRequestHandler
    : IRequestHandler<AdvanceWarrantRequest, AdvanceWarrantResponse>
{
    private readonly IRepository<Warrant> _warrants;

    public AdvanceWarrantRequestHandler(IRepository<Warrant> warrants)
    {
        _warrants = warrants;
    }

    public async Task<AdvanceWarrantResponse> Handle(
        AdvanceWarrantRequest request, 
        CancellationToken cancellationToken)
    {
        GetWarrantSpecification query = new(request.WarrantId);

        Warrant? warrant = 
            await _warrants.FirstOrDefaultAsync(query, cancellationToken);

        if (warrant is null)
        {
            throw new EntityNotFoundException<Warrant, Guid>(request.WarrantId);
        }

        warrant.AdvanceToStep(request.StepId);

        await _warrants.SaveChangesAsync(cancellationToken);

        return new AdvanceWarrantResponse();
    }
}
