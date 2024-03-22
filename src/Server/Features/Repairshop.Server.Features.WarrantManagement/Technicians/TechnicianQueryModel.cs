using Repairshop.Server.Features.WarrantManagement.Warrants;

namespace Repairshop.Server.Features.WarrantManagement.Technicians;

internal class TechnicianQueryModel
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required IEnumerable<WarrantQueryModel> Warrants { get; set; }
}
