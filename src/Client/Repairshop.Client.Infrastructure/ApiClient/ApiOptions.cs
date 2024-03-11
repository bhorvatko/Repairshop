namespace Repairshop.Client.Infrastructure.ApiClient;
public class ApiOptions
{
    public const string SectionName = "Api";

    public required string BaseAddress { get; set; }
    public required string ApiKey { get; set; }
}
