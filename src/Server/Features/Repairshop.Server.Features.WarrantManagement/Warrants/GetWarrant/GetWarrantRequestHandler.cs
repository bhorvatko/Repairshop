using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.GetWarrant;

internal class GetWarrantRequestHandler
    : IRequestHandler<GetWarrantRequest, GetWarrantResponse>
{
    private readonly IRepository<Warrant> _warrants;

    public GetWarrantRequestHandler(IRepository<Warrant> warrants)
    {
        _warrants = warrants;
    }

    public async Task<GetWarrantResponse> Handle(
        GetWarrantRequest request, 
        CancellationToken cancellationToken)
    {
        GetWarrantResponseSpecification specification =
            new(request.Id);

        GetWarrantResponse? warrantResponse = 
            await _warrants.FirstOrDefaultAsync(specification, cancellationToken);

        if (warrantResponse is null)
        {
            throw new EntityNotFoundException<Warrant, Guid>(request.Id);
        }

        return warrantResponse;
    }
}
