namespace Blazinga.Samples;

public class VersionHelper
{
    public static string GetVersion()
    {
        var assembly = typeof(Blazinga.Components.EnumSelect<>).Assembly;
        var version = assembly.GetName().Version?.ToString() ?? "Unknown";
        return version;
    }
}
