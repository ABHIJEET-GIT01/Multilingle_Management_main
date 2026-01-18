namespace WebAPI_NOVOAssignment.Extensions;

/// <summary>
/// Extension methods for HTTP context to get culture
/// </summary>
public static class HttpContextExtensions
{
    private const string CultureQueryParam = "culture";
    private const string CultureHeader = "X-Culture";
    private const string DefaultCulture = "en";

    public static string GetCulture(this HttpContext context)
    {
        // Check query parameter first
        if (context.Request.Query.TryGetValue(CultureQueryParam, out var cultureQuery))
        {
            var culture = cultureQuery.ToString().ToLowerInvariant();
            if (!string.IsNullOrEmpty(culture))
                return culture;
        }

        // Check header
        if (context.Request.Headers.TryGetValue(CultureHeader, out var cultureHeader))
        {
            var culture = cultureHeader.ToString().ToLowerInvariant();
            if (!string.IsNullOrEmpty(culture))
                return culture;
        }

        // Return default culture
        return DefaultCulture;
    }
}
