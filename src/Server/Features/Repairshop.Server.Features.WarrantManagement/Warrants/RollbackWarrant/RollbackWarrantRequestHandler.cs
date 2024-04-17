using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.RollbackWarrant;

internal class RollbackWarrantRequestHandler
    : IRequestHandler<RollbackWarrantRequest, RollbackWarrantResponse>
{
    private readonly IRepository<Warrant> _warrants;

    public RollbackWarrantRequestHandler(IRepository<Warrant> warrants)
    {
        _warrants = warrants;
    }

    public async Task<RollbackWarrantResponse> Handle(
        RollbackWarrantRequest request, 
        CancellationToken cancellationToken)
    {
        GetWarrantSpecification query = new(request.WarrantId);

        Warrant? warrant =
            await _warrants.FirstOrDefaultAsync(query, cancellationToken);

        if (warrant is null)
        {
            throw new EntityNotFoundException<Warrant, Guid>(request.WarrantId);
        }

        warrant.RollbackToStep(request.StepId);

        await _warrants.SaveChangesAsync(cancellationToken);

        return new RollbackWarrantResponse();
    }
}
