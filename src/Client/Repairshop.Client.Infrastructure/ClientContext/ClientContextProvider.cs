using Repairshop.Client.Common.ClientContext;

namespace Repairshop.Client.Infrastructure.ClientContext;

internal class ClientContextProvider
    : IClientContextProvider
{
    public string ClientContext { get; private set; }

    public ClientContextProvider(string clientContext)
    {
        ClientContext = clientContext;
    }

    public string GetClientContext() => ClientContext;
}
