namespace Blazinga.Components;
public partial class CultureSwitcher
{
    [Inject] IJSRuntime JS { get; set; } = default!;
    [Inject] NavigationManager Nav { get; set; } = default!;

    private string currentCulture = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        currentCulture = CultureInfo.CurrentUICulture.Name; //await JS.InvokeAsync<string>("localStorage.getItem", "culture");
    }

    private async Task SetCultureAsync(string culture)
    {
        await JS.InvokeVoidAsync("localStorage.setItem", "culture", culture);
        Nav.NavigateTo(Nav.Uri, forceLoad: true);
    }
}
