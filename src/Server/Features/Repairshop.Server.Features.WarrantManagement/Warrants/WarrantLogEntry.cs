using MediatR;
using Repairshop.Server.Common.Entities;

namespace Repairshop.Server.Features.WarrantManagement.Warrants;

public class WarrantLogEntry
    : EntityBase, INotification
{
#nullable disable
    private WarrantLogEntry() { }
#nullable enable

    public Guid Id { get; private set; }
    public int WarrantNumber { get; private set; }
    public string? PreviousState { get; private set; }
    public string NewState { get; private set; }
    public string? TechnicianName { get; private set; }
    public DateTimeOffset EventTime { get; private set; }

    public static WarrantLogEntry CreateCreatedLogEntry(
        int warrantNumber,
        string newState,
        Func<DateTimeOffset> getUtcNow)
    {
        return new WarrantLogEntry()
        {
            Id = Guid.NewGuid(),
            WarrantNumber = warrantNumber,
            NewState = newState,
            EventTime = getUtcNow()
        };
    }

    public static WarrantLogEntry CreateUpdatedLogEntry(
        int warrantNumber,
        string previousState,
        string newState,
        string technicianName,
        Func<DateTimeOffset> getUtcNow)
    {
        return new WarrantLogEntry()
        {
            Id = Guid.NewGuid(),
            WarrantNumber = warrantNumber,
            PreviousState = previousState,
            NewState = newState,
            TechnicianName = technicianName,
            EventTime = getUtcNow()
        };
    }
}
