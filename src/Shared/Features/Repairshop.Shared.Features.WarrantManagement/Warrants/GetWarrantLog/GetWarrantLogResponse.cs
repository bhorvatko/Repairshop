namespace Repairshop.Shared.Features.WarrantManagement.Warrants.GetWarrantLog;

public class GetWarrantLogResponse
{
    public required IReadOnlyCollection<WarrantLogEntryModel> LogEntries { get; init; }
}