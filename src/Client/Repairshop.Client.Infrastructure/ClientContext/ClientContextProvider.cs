namespace Repairshop.Client.Infrastructure.ClientContext;

internal class ClientContextProvider
{
    public string ClientContext { get; private set;  }

    public ClientContextProvider(string clientContext)
    {
        ClientContext = clientContext;
    }
}
