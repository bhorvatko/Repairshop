namespace Repairshop.Shared.Features.WarrantManagement.Warrants;
public class GetWarrantsResponse
{
    public required IEnumerable<WarrantModel> Warrants { get; set; }
}
