using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants;

internal class GetWarrantsRequestHandler
    : IRequestHandler<GetWarrantsRequest, GetWarrantsResponse>
{
    private readonly IRepository<Warrant> _warrants;

    public GetWarrantsRequestHandler(IRepository<Warrant> warrants)
    {
        _warrants = warrants;
    }

    public async Task<GetWarrantsResponse> Handle(
        GetWarrantsRequest request, 
        CancellationToken cancellationToken)
    {
        GetWarrantModelsSpecification specification = 
            new(technicianId: request.TechnicianId);

        return new GetWarrantsResponse()
        {
            Warrants = await _warrants.ListAsync(specification, cancellationToken)
        };
    }
}
