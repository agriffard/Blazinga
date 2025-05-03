namespace Blazinga.Samples;

public class VersionHelper
{
    public static string GetVersion()
    {
        var assembly = typeof(Blazinga.Components.EnumSelect<>).Assembly;
        var version = assembly.GetName().Version?.ToString() ?? "Unknown";
        // Display only Major.Minor.Patch
        var versionParts = version.Split('.');
        if (versionParts.Length > 3)
        {
            version = string.Join(".", versionParts[0..3]);
        }
        return version;
    }
}
