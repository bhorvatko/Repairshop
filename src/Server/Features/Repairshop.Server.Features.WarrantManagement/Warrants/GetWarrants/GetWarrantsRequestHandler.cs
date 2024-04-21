using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.GetWarrants;

internal class GetWarrantsRequestHandler
    : IRequestHandler<GetWarrantsRequest, GetWarrantsResponse>
{
    private readonly IRepository<Warrant> _warrants;

    public GetWarrantsRequestHandler(
        IRepository<Warrant> warrants)
    {
        _warrants = warrants;
    }

    public async Task<GetWarrantsResponse> Handle(
        GetWarrantsRequest request,
        CancellationToken cancellationToken)
    {
        GetWarrantModelsSpecification specification =
            new(technicianId: request.TechnicianId);

        IEnumerable<WarrantModel> warrantModels =
            await _warrants.ListAsync(specification, cancellationToken);

        return new GetWarrantsResponse()
        {
            Warrants = warrantModels
        };
    }
}
