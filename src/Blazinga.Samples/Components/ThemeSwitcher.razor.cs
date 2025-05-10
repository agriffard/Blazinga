namespace Blazinga.Samples.Components;
public partial class ThemeSwitcher
{
    [Inject] ThemeService ThemeService { get; set; } = default!;

    private string GetThemeCssClass()
    {
        return ThemeService.CurrentTheme switch
        {
            "light" => "sun-fill",
            "dark" => "moon-stars-fill",
            _ => "circle-half"
        };
    }

    private async Task SetThemeAsync(string theme)
    {
        await ThemeService.SetThemeAsync(theme);
    }
}
