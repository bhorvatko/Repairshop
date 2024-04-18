using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.UnassignWarrant;

internal class UnassignWarrantRequestHandler
    : IRequestHandler<UnassignWarrantRequest, UnassignWarrantResponse>
{
    private readonly IRepository<Warrant> _warrants;

    public UnassignWarrantRequestHandler(IRepository<Warrant> warrants)
    {
        _warrants = warrants;
    }

    public async Task<UnassignWarrantResponse> Handle(
        UnassignWarrantRequest request, 
        CancellationToken cancellationToken)
    {
        GetWarrantSpecification specification = 
            new(request.Id);

        Warrant? warrant = 
            await _warrants.FirstOrDefaultAsync(specification, cancellationToken);

        if (warrant is null)
        {
            throw new EntityNotFoundException<Warrant, Guid>(request.Id);
        }

        warrant.UnassignWarrant();

        await _warrants.SaveChangesAsync(cancellationToken);

        return new UnassignWarrantResponse();
    }
}
