namespace Repairshop.Server.Infrastructure.Authorization.ApiKey;

public class ApiKeyOptions
{
    public const string SectionName = "ApiKey";

    public required IEnumerable<string> ValidKeys { get; set; }
}
