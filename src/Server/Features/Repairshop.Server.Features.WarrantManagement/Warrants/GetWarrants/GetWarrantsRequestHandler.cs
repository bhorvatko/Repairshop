using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.GetWarrants;

internal class GetWarrantsRequestHandler
    : IRequestHandler<GetWarrantsRequest, GetWarrantsResponse>
{
    private readonly IRepository<Warrant> _warrants;
    private readonly WarrantModelFactory _warrantModelFactory;

    public GetWarrantsRequestHandler(
        IRepository<Warrant> warrants,
        WarrantModelFactory warrantModelFactory)
    {
        _warrants = warrants;
        _warrantModelFactory = warrantModelFactory;
    }

    public async Task<GetWarrantsResponse> Handle(
        GetWarrantsRequest request,
        CancellationToken cancellationToken)
    {
        GetWarrantModelsSpecification specification =
            new(technicianId: request.TechnicianId);

        IEnumerable<WarrantQueryModel> queryModels =
            await _warrants.ListAsync(specification, cancellationToken);

        return new GetWarrantsResponse()
        {
            Warrants = _warrantModelFactory.Create(queryModels)
        };
    }
}
