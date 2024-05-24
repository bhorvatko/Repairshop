using MediatR;
using Repairshop.Server.Common.Exceptions;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Procedures;
using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.GetWarrant;

internal class GetWarrantRequestHandler
    : IRequestHandler<GetWarrantRequest, GetWarrantResponse>
{
    private readonly IRepository<Warrant> _warrants;

    public GetWarrantRequestHandler(IRepository<Warrant> warrants)
    {
        _warrants = warrants;
    }

    public async Task<GetWarrantResponse> Handle(
        GetWarrantRequest request, 
        CancellationToken cancellationToken)
    {
        GetWarrantSpecification specification =
            new(request.Id);

        Warrant? warrant = 
            await _warrants.FirstOrDefaultAsync(specification, cancellationToken);

        if (warrant is null)
        {
            throw new EntityNotFoundException<Warrant, Guid>(request.Id);
        }

        return new GetWarrantResponse()
        {
            Id = warrant.Id,
            Deadline = warrant.Deadline,
            IsUrgent = warrant.IsUrgent,
            Title = warrant.Title,
            WarrantSteps = warrant
                .GetStepsInSequence()
                .Select(s => new WarrantStepModel()
                {
                    CanBeTransitionedToByFrontOffice =
                    s.PreviousTransition != null
                        ? s.PreviousTransition.CanBePerformedByFrontOffice
                        : false,
                    CanBeTransitionedToByWorkshop =
                    s.PreviousTransition != null
                        ? s.PreviousTransition.CanBePerformedByWorkshop
                        : false,
                    Procedure = new ProcedureSummaryModel()
                    {
                        Id = s.ProcedureId,
                        Color = s.Procedure.Color,
                        Name = s.Procedure.Name,
                    }
                })
        };
    }
}
