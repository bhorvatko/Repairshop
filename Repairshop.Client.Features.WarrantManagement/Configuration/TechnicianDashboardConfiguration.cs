namespace Repairshop.Client.Features.WarrantManagement.Configuration;

public record TechnicianDashboardConfiguration
{
    public required Guid? TechnicianId { get; set; }
    public required IReadOnlyCollection<Guid> ProcedureFilters { get; set; }
        = new List<Guid>();

    public static TechnicianDashboardConfiguration CreateDefault() =>
        new TechnicianDashboardConfiguration()
        {
            TechnicianId = null,
            ProcedureFilters = new List<Guid>()
        };
}
