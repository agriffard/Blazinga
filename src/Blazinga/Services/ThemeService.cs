namespace Blazinga.Services;

public class ThemeService
{
    private const string ThemeKey = "theme";
    private readonly IJSRuntime _js;

    public string CurrentTheme { get; private set; } = "auto";

    public ThemeService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task InitAsync()
    {
        var savedTheme = await _js.InvokeAsync<string>("localStorage.getItem", ThemeKey);
        if (!string.IsNullOrEmpty(savedTheme))
        {
            CurrentTheme = savedTheme;
        }
        await ApplyThemeAsync(CurrentTheme);
    }

    public async Task SetThemeAsync(string theme)
    {
        CurrentTheme = theme;
        await _js.InvokeVoidAsync("localStorage.setItem", ThemeKey, theme);
        await ApplyThemeAsync(theme);
    }

    private async Task ApplyThemeAsync(string theme)
    {
        await _js.InvokeVoidAsync("setBootstrapTheme", theme);
    }
}
