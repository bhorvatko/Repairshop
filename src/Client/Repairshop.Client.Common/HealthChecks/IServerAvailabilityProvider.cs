namespace Repairshop.Client.Common.HealthChecks;

public interface IServerAvailabilityProvider
{
    IDisposable SubscribeToServerAvailability(Action<bool> onServerAvailabilityChange);
}
