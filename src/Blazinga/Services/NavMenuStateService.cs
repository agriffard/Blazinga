namespace Blazinga.Services;

public class NavMenuStateService
{
    private readonly IJSRuntime _js;
    private const string Key = "navCollapsed";

    public bool IsCollapsed { get; private set; }

    public event Action? OnChange;

    public NavMenuStateService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task InitializeAsync()
    {
        var result = await _js.InvokeAsync<string>("localStorage.getItem", Key);
        IsCollapsed = result == "true";
        OnChange?.Invoke();
    }

    public async Task ToggleAsync()
    {
        IsCollapsed = !IsCollapsed;
        await _js.InvokeVoidAsync("localStorage.setItem", Key, IsCollapsed.ToString().ToLower());
        OnChange?.Invoke();
    }
}
