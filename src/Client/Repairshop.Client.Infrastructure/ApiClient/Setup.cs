using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace Repairshop.Client.Infrastructure.ApiClient;

internal static class Setup
{
    public static IServiceCollection AddApiClient(
        this IServiceCollection services,
        IConfiguration config) =>
        services
            .AddSingleton<RestClient>(sp =>
            {
                ApiOptions apiOptions = config
                    .GetSection(ApiOptions.SectionName)
                    .Get<ApiOptions>()!;

                RestClientOptions restClientOptions =
                    new RestClientOptions(apiOptions.BaseAddress);

                return new RestClient(restClientOptions);
            })
            .AddSingleton<ApiClient>();
}
