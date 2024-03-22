namespace Repairshop.Shared.Common.ClientContext;

public class RepairshopClientContext
{
    public const string FrontOffice = nameof(FrontOffice);
    public const string Workshop = nameof(Workshop);
    
    public static IEnumerable<string> All = new[]
    {
        FrontOffice,
        Workshop
    };
}
