using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using Repairshop.Client.Common.ClientContext;
using Repairshop.Shared.Common.ClientContext;
using Repairshop.Shared.Common.HealthChecks;
using Repairshop.Shared.Common.Notifications;
using RestSharp;
using System.Net;

namespace Repairshop.Client.Infrastructure.ApiClient;
internal class ApiClient
{
    private readonly ApiOptions _apiOptions;
    private readonly RestClient _restClient;
    private readonly IClientContextProvider _clientContextProvider;
    private readonly HubConnection _hubConnection;

    public ApiClient(
        IOptions<ApiOptions> apiOptions, 
        RestClient restClient,
        IClientContextProvider clientContextProvider,
        HubConnection hubConnection)
    {
        _apiOptions = apiOptions.Value;
        _restClient = restClient;
        _clientContextProvider = clientContextProvider;
        _hubConnection = hubConnection;
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

    public async Task<bool> CheckHealth()
    {
        RestRequest restRequest = CreateRequest(HealthCheckConstants.HeathlCheckEndpoint);

        RestResponse response = await _restClient.ExecuteAsync(restRequest);
  
        return response.IsSuccessful && response.StatusCode == HttpStatusCode.OK;
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

    private RestRequest CreateRequest(string resource)
    {
        RestRequest request = new RestRequest(resource)
            .AddHeader("X-API-KEY", _apiOptions.ApiKey)
            .AddHeader(
                ClientContextConstants.ClientContextHeader,
                _clientContextProvider.GetClientContext());

        if (_hubConnection.ConnectionId is not null)
        {
            request.AddHeader(
                NotificationConstants.HubConnectionIdHeader,
                _hubConnection.ConnectionId);
        }

        return request;
    }
}
