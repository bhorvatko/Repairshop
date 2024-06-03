using MediatR;
using Repairshop.Server.Common.Persistence;
using Repairshop.Shared.Features.WarrantManagement.Warrants.GetWarrantLog;

namespace Repairshop.Server.Features.WarrantManagement.Warrants.GetWarrantLog;

internal class GetWarrantLogRequestHandler
    : IRequestHandler<GetWarrantLogRequest, GetWarrantLogResponse>
{
    private readonly IRepository<WarrantLogEntry> _warrantLogEntries;

    public GetWarrantLogRequestHandler(IRepository<WarrantLogEntry> warrantLogEntries)
    {
        _warrantLogEntries = warrantLogEntries;
    }

    public async Task<GetWarrantLogResponse> Handle(
        GetWarrantLogRequest request,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<WarrantLogEntry> result =
            await _warrantLogEntries.ListAsync(cancellationToken);

        return new GetWarrantLogResponse()
        {
            LogEntries = 
                result.Select(x => new WarrantLogEntryModel()
                {
                    EventTime = x.EventTime,
                    NewState = x.NewState,
                    WarrantNumber = x.WarrantNumber,
                    PreviousState = x.PreviousState,
                    TechnicianName = x.TechnicianName 
                })
                .ToList()
        };
    }
}
