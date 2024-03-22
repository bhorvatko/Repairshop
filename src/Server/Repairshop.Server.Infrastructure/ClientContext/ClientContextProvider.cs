using Microsoft.AspNetCore.Http;
using Repairshop.Server.Common.ClientContext;
using Repairshop.Shared.Common.ClientContext;

namespace Repairshop.Server.Infrastructure.ClientContext;

internal class ClientContextProvider
    : IClientContextProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClientContextProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetClientContext()
    {
        HttpContext httpContext = _httpContextAccessor.HttpContext!;

        if (!httpContext.Request.Headers.TryGetValue(ClientContextConstants.ClientContextHeader, out var clientContextStr))
        {
            throw new MissingClientContextException();
        }

        if (!RepairshopClientContext.All.Contains(clientContextStr.ToString()))
        {
            throw new InvalidClientContextException();
        }

        return clientContextStr.ToString();
    }
}
