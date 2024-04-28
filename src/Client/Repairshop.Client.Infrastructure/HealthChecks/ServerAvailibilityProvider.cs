using Repairshop.Client.Common.HealthChecks;
using System.Reactive.Linq;

namespace Repairshop.Client.Infrastructure.HealthChecks;

internal class ServerAvailibilityProvider
    : IServerAvailabilityProvider
{
    private readonly IObservable<bool> _serverAvailableObservable;
    private readonly ApiClient.ApiClient _apiClient;

    public ServerAvailibilityProvider(ApiClient.ApiClient apiClient)
    {
        _serverAvailableObservable = Observable
            .Interval(TimeSpan.FromSeconds(5))
            .SelectMany(_ => CheckServerAvailability());

        _apiClient = apiClient;
    }

    public IDisposable SubscribeToServerAvailability(Action<bool> onServerAvailabilityChange) =>
        _serverAvailableObservable.Subscribe(onServerAvailabilityChange);

    private Task<bool> CheckServerAvailability() =>
        _apiClient.CheckHealth();
}
