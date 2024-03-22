using Repairshop.Shared.Common.ClientContext;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;

namespace System.Net.Http;

public static class HttpClientExtensions
{
    public static Task<TValue?> GetFromJsonAsync<TValue>(
        this HttpClient client, 
        [StringSyntax(StringSyntaxAttribute.Uri)] string requestUri, 
        object request)
    {
        return client.GetFromJsonAsync<TValue>(requestUri.AppendQuery(request));
    }

    public static void AddClientContextHeader(
        this HttpClient client, 
        string clientContext) =>
        client.DefaultRequestHeaders.Add(ClientContextConstants.ClientContextHeader, clientContext);
}
