using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Technicians;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;

internal class GetTechniciansRequestHandler
    : IRequestHandler<GetTechniciansRequest, GetTechniciansResponse>
{
    private readonly IRepository<Technician> _technicians;

    public GetTechniciansRequestHandler(
        IRepository<Technician> technicians)
    {
        _technicians = technicians;
    }

    public async Task<GetTechniciansResponse> Handle(
        GetTechniciansRequest request, 
        CancellationToken cancellationToken)
    {
        GetTechnicianModelsSpecifcation specification = new();

        IEnumerable<TechnicianModel> technicianModels =
            await _technicians.ListAsync(specification, cancellationToken);

        return new GetTechniciansResponse()
        {
            Technicians = technicianModels
        };
    }
}
