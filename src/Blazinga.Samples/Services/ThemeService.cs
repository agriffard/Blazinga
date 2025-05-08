namespace Blazinga.Samples.Services;

public class ThemeService
{
    //public static readonly string[] ThemeOptions = [Light, Dark, Auto];

    public const string Light = "light";
    public const string Dark = "dark";
    public const string Auto = "auto";

    public string CurrentTheme { get; private set; } = Auto;

    public event Action? OnThemeChanged;

    public void SetTheme(string theme)
    {
        if (theme != Light && theme != Dark && theme != Auto)
            throw new ArgumentException("Invalid theme value.");

        CurrentTheme = theme;
        OnThemeChanged?.Invoke();
    }
}
