using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Repairshop.Shared.Features.WarrantManagement.Technicians;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;

internal class AssignWarrantRequestHandler
    : IRequestHandler<AssignWarrantRequest, AssignWArrantResponse>
{
    private readonly IRepository<Technician> _technicians;
    private readonly IRepository<Warrant> _warrants;

    public AssignWarrantRequestHandler(
        IRepository<Technician> technicians, 
        IRepository<Warrant> warrants)
    {
        _technicians = technicians;
        _warrants = warrants;
    }

    public async Task<AssignWArrantResponse> Handle(
        AssignWarrantRequest request, 
        CancellationToken cancellationToken)
    {
        Technician? technician = 
            await _technicians.GetByIdAsync(request.TechnicianId, cancellationToken);
        
        if (technician is null)
        {
            throw new EntityNotFoundException<Technician, Guid>(request.TechnicianId);
        }

        GetWarrantSpecification getWarrantSpecification =
            new(request.WarrantId);

        Warrant? warrant =
            await _warrants.FirstOrDefaultAsync(getWarrantSpecification, cancellationToken);

        if (warrant is null)
        {
            throw new EntityNotFoundException<Warrant, Guid>(request.WarrantId);
        }

        technician.AssignWarrant(warrant);

        await _technicians.UpdateAsync(technician, cancellationToken);

        return new AssignWArrantResponse();
    }
}
