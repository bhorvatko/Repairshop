using Microsoft.AspNetCore.Http;
using Repairshop.Shared.Common.Notifications;

namespace Repairshop.Server.Infrastructure.Notifications;

internal class HubConnectionIdProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HubConnectionIdProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GethubConnectionId()
    {
        HttpContext? httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null) return null;

        if (!httpContext.Request.Headers.TryGetValue(
            NotificationConstants.HubConnectionIdHeader, 
            out var hubConnectionId))
        {
            return null;
        }

        return hubConnectionId;
    }
}
