using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Technicians;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;

internal class UpdateTechnicianRequestHandler
    : IRequestHandler<UpdateTechnicianRequest, UpdateTechnicianResponse>
{
    private readonly IRepository<Technician> _technicians;

    public UpdateTechnicianRequestHandler(IRepository<Technician> technicians)
    {
        _technicians = technicians;
    }

    public async Task<UpdateTechnicianResponse> Handle(
        UpdateTechnicianRequest request, 
        CancellationToken cancellationToken)
    {
        Technician? technician = 
            await _technicians.GetByIdAsync(request.TechnicianId, cancellationToken);

        if (technician is null)
        {
            throw new EntityNotFoundException<Technician, Guid>(request.TechnicianId);
        }

        technician.Update(request.Name);

        await _technicians.UpdateAsync(technician, cancellationToken);

        return new UpdateTechnicianResponse();
    }
}
