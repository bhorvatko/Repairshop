using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Warrants;
using static Repairshop.Server.Features.WarrantManagement.Warrants.WarrantStep;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.CreateWarrant;

internal class CreateWarrantRequestHandler
    : IRequestHandler<CreateWarrantRequest, CreateWarrantResponse>
{
    private readonly IRepository<Warrant> _warrants;
    private readonly IRepository<Procedure> _procedures;

    public CreateWarrantRequestHandler(
        IRepository<Warrant> warrants,
        IRepository<Procedure> procedures)
    {
        _warrants = warrants;
        _procedures = procedures;
    }

    public async Task<CreateWarrantResponse> Handle(
        CreateWarrantRequest request,
        CancellationToken cancellationToken)
    {
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

        Warrant warrant = await Warrant.Create(
            request.Title,
            request.Deadline,
            request.IsUrgnet,
            stepSequence,
            warrant => _warrants.AddAsync(warrant, cancellationToken));

        await _warrants.SaveChangesAsync(cancellationToken);

        return new CreateWarrantResponse();
    }
}
