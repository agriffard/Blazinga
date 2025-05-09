using Blazinga.Samples.Services;

namespace Blazinga.Samples.Layout;
public partial class ThemeSwitcher
{
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
