namespace Repairshop.Shared.Features.WarrantManagement.Warrants.GetWarrantLog;

public class WarrantLogEntryModel
{
    public required int WarrantNumber { get; init; }
    public string? PreviousState { get; init; }
    public required string NewState { get; init; }
    public string? TechnicianName { get; init; }
    public required DateTimeOffset EventTime { get; init; }
}
