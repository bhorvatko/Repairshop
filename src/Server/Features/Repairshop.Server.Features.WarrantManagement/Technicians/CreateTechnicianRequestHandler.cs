using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Technicians;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;

internal class CreateTechnicianRequestHandler
    : IRequestHandler<CreateTechnicianRequest, CreateTechnicianResponse>
{
    private readonly IRepository<Technician> _technicians;

    public CreateTechnicianRequestHandler(IRepository<Technician> technicians)
    {
        _technicians = technicians;
    }

    public async Task<CreateTechnicianResponse> Handle(
        CreateTechnicianRequest request, 
        CancellationToken cancellationToken)
    {
        Technician technician = Technician.Create(request.Name);

        await _technicians.AddAsync(technician);

        return new CreateTechnicianResponse();
    }
}
