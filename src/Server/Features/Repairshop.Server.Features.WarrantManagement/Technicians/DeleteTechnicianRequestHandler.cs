using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Technicians;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;

internal class DeleteTechnicianRequestHandler
    : IRequestHandler<DeleteTechnicianRequest, DeleteTechnicianResponse>
{
    private readonly IRepository<Technician> _techniciansRepository;

    public DeleteTechnicianRequestHandler(IRepository<Technician> techniciansRepository)
    {
        _techniciansRepository = techniciansRepository;
    }

    public async Task<DeleteTechnicianResponse> Handle(
        DeleteTechnicianRequest request, 
        CancellationToken cancellationToken)
    {
        GetTechnicianSpecification specification = new(request.Id);

        Technician? technician = 
            await _techniciansRepository.FirstOrDefaultAsync(specification, cancellationToken);

        if (technician is null)
        {
            throw new EntityNotFoundException<Technician, Guid>(request.Id);
        }

        technician.UnassignAllWarrants();

        await _techniciansRepository.DeleteAsync(technician, cancellationToken);

        return new DeleteTechnicianResponse();
    }
}
