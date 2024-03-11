namespace Repairshop.Shared.Features.WarrantManagement.Technicians;

public class GetTechniciansResponse
{
    public required IEnumerable<TechnicianModel> Technicians { get; set; }
}
