namespace Arbor.CodeAutomate;

internal static class StringExtensions
{
    public static string ConvertToPath(this Uri uri)
    {
        var parts = new List<string> { uri.Host  };
        parts.AddRange(uri.PathAndQuery.Split('?')[0].Split('/'));
        return string.Join("_", parts);
    }
}