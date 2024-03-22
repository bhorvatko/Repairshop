using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Reflection;

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

    private static string GetQueryParameterString(
        this PropertyInfo property, 
        object request) =>
        property.Name + "=" + property.GetValue(request)?.ToString() ?? "";
}
