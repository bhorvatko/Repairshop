using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Shared.Features.WarrantManagement.Technicians;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;

internal class GetTechniciansRequestHandler
    : IRequestHandler<GetTechniciansRequest, GetTechniciansResponse>
{
    private readonly IRepository<Technician> _technicians;
    private readonly WarrantModelFactory _warrantModelFactory;

    public GetTechniciansRequestHandler(
        IRepository<Technician> technicians,
        WarrantModelFactory warrantModelFactory)
    {
        _technicians = technicians;
        _warrantModelFactory = warrantModelFactory;
    }

    public async Task<GetTechniciansResponse> Handle(
        GetTechniciansRequest request, 
        CancellationToken cancellationToken)
    {
        GetTechnicianModelsSpecifcation specification = new();

        IEnumerable<TechnicianQueryModel> queryModels =
            await _technicians.ListAsync(specification, cancellationToken);

        return new GetTechniciansResponse()
        {
            Technicians = queryModels.Select(x => new TechnicianModel()
            {
                Id = x.Id,
                Name = x.Name,
                Warrants = _warrantModelFactory.Create(x.Warrants)
            })
        };
    }
}
