namespace Repairshop.Client.Features.WarrantManagement.WarrantLog;

public class WarrantLogEntryViewModel
{
    public required DateTime EventTime { get; init; }
    public required int WarrantNumber { get; init; }
    public required string EventDescription { get; init; }
    public string? TechnicianName { get; init; }

    public string FormattedEventTime => EventTime.ToString("dd.MM.yyyy. HH:mm");
}
