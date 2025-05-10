namespace Blazinga.Samples.Components;
public partial class GitHubSourceLink
{
    private string? SourceUrl;

    protected override void OnInitialized()
    {
        Navigation.LocationChanged += OnLocationChanged;
        UpdateSourceUrl(Navigation.Uri);
    }

    private void OnLocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
    {
        UpdateSourceUrl(e.Location);
        InvokeAsync(StateHasChanged);
    }

    private void UpdateSourceUrl(string uri)
    {
        var relativeUri = Navigation.ToBaseRelativePath(uri);
        var pageName = relativeUri.Split('?')[0].Split('#')[0];

        if (!string.IsNullOrWhiteSpace(pageName))
        {
            var filename = char.ToUpper(pageName[0]) + pageName[1..] + ".razor";
            SourceUrl = $"https://github.com/agriffard/Blazinga/blob/main/src/Blazinga.Samples/Pages/{filename}";
        }
        else
        {
            SourceUrl = null;
        }
    }

    public void Dispose()
    {
        Navigation.LocationChanged -= OnLocationChanged;
    }
}
