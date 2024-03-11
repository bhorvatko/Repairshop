using Repairshop.Shared.Features.WarrantManagement.Warrants;

namespace Repairshop.Shared.Features.WarrantManagement.Technicians;

public class TechnicianModel
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required IEnumerable<WarrantModel> Warrants { get; set; }
}


