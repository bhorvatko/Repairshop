using System.Reflection;

namespace System;

public static class StringExtensions
{
    public static string AppendQuery(this string str, object request)
    {
        IEnumerable<string> queryParameters =
            request.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.GetQueryParameterString(request));

        string queryString = string.Join('&', queryParameters);

        return str + "?" + queryString;
    }

    private static string GetQueryParameterString(
        this PropertyInfo property,
        object request) =>
        property.Name + "=" + property.GetValue(request)?.ToString() ?? "";
}
