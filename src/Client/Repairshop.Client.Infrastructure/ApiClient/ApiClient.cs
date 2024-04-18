using Microsoft.Extensions.Options;
using Repairshop.Client.Infrastructure.ClientContext;
using Repairshop.Shared.Common.ClientContext;
using RestSharp;

namespace Repairshop.Client.Infrastructure.ApiClient;
internal class ApiClient
{
    private readonly ApiOptions _apiOptions;
    private readonly RestClient _restClient;
    private readonly ClientContextProvider _clientContextProvider;

    public ApiClient(
        IOptions<ApiOptions> apiOptions, 
        RestClient restClient,
        ClientContextProvider clientContextProvider)
    {
        _apiOptions = apiOptions.Value;
        _restClient = restClient;
        _clientContextProvider = clientContextProvider;
    }

    public async Task<TResponse> Post<TRequest, TResponse>(
        string resource,
        TRequest request)
        where TRequest : class
    {
        RestRequest restRequest = 
            CreateRequest(resource).AddJsonBody(request);

        TResponse? response = await _restClient.PostAsync<TResponse>(restRequest);

        if (response is null)
        {
            throw new InvalidOperationException($"The received response for POST {resource} is empty.");
        }

        return response;
    }

    public async Task<TResponse> Get<TResponse>(string resource)
    {
        RestRequest restRequest = CreateRequest(resource);

        TResponse? response = await _restClient.GetAsync<TResponse>(restRequest);

        if (response is null)
        {
            throw new InvalidOperationException($"The received response for GET {resource} is empty.");
        }

        return response;
    }

    public Task<TResponse> Get<TResponse>(string resource, object request) =>
        Get<TResponse>(resource.AppendQuery(request));

    public async Task<TResponse> Put<TRequest, TResponse>(
        string resource,
        TRequest request)
        where TRequest : class
    {
        RestRequest restRequest =
            CreateRequest(resource).AddJsonBody(request);

        TResponse? response = await _restClient.PutAsync<TResponse>(restRequest);

        if (response is null)
        {
            throw new InvalidOperationException($"The received response for PUT {resource} is empty.");
        }

        return response;
    }

    public async Task<TResponse> Put<TResponse>(string resource)
    {
        RestRequest restRequest =
            CreateRequest(resource);

        TResponse? response = await _restClient.PutAsync<TResponse>(restRequest);

        if (response is null)
        {
            throw new InvalidOperationException($"The received response for PUT {resource} is empty.");
        }

        return response;
    }

    private RestRequest CreateRequest(string resource) =>
        new RestRequest(resource)
            .AddHeader("X-API-KEY", _apiOptions.ApiKey)
            .AddHeader(
                ClientContextConstants.ClientContextHeader, 
                _clientContextProvider.ClientContext);
}
