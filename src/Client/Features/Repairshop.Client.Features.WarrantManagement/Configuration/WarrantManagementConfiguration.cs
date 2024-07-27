namespace Repairshop.Client.Features.WarrantManagement.Configuration;

public record WarrantManagementConfiguration
{
    public IEnumerable<TechnicianDashboardConfiguration> TechnicianDashboards { get; set; } =
        Enumerable.Empty<TechnicianDashboardConfiguration>();
}
