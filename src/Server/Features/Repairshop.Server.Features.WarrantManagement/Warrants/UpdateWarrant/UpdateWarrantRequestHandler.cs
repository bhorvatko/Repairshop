using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using static Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStep;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.UpdateWarrant;

internal class UpdateWarrantRequestHandler
    : IRequestHandler<UpdateWarrantRequest, UpdateWarrantResponse>
{
    private readonly IRepository<Warrant> _warrants;
    private readonly IRepository<Procedure> _procedures;

    public UpdateWarrantRequestHandler(
        IRepository<Warrant> warrants,
        IRepository<Procedure> procedures)
    {
        _warrants = warrants;
        _procedures = procedures;
    }

    public async Task<UpdateWarrantResponse> Handle(
        UpdateWarrantRequest request,
        CancellationToken cancellationToken)
    {
        GetWarrantSpecification specification = new(request.Id);

        Warrant? warrant =
            await _warrants.FirstOrDefaultAsync(specification, cancellationToken);

        if (warrant is null)
        {
            throw new EntityNotFoundException<Warrant, Guid>(request.Id);
        }

        IEnumerable<CreateWarrantStepArgs> warrantStepArgs =
            request.Steps
                .Select(x => new CreateWarrantStepArgs(
                    x.ProcedureId,
                    x.CanBeTransitionedToByFrontDesk,
                    x.CanBeTransitionedToByWorkshop));

        GetProceduresByIdDelegate getProceduresById =
            async ids => await _procedures.ListAsync(new GetProceduresSpecification(ids), cancellationToken);

        IEnumerable<WarrantStep> stepSequence =
            await CreateStepSequence(
                warrantStepArgs,
                getProceduresById);

        warrant.Update(
            request.Title,
            request.Deadline,
            request.IsUrgent,
            stepSequence);

        await _warrants.SaveChangesAsync(cancellationToken);

        warrant.SetCurrentStepByProcedureId(request.CurrentStepProcedureId);
        
        await _warrants.SaveChangesAsync(cancellationToken);

        return new UpdateWarrantResponse();
    }
}
