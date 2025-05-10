namespace Blazinga.Components;
public partial class Typeahead<TItem>
{
    [Parameter] public Func<string, Task<List<TItem>>> SearchFunction { get; set; } = default!;
    [Parameter] public EventCallback<TItem?> SelectedChanged { get; set; }
    [Parameter] public Func<TItem, string> ItemSelector { get; set; } = item => item?.ToString() ?? string.Empty;
    [Parameter] public string Placeholder { get; set; } = "Search...";
    [Parameter] public TItem? SelectedItem { get; set; }

    private List<TItem> FilteredItems { get; set; } = new();
    private string SearchText { get; set; } = string.Empty;
    private bool ShowSuggestions { get; set; } = false;
    private CancellationTokenSource? DebounceCts;

    protected override async Task OnParametersSetAsync()
    {
        if (SelectedItem != null)
        {
            SearchText = ItemSelector(SelectedItem);
        }
    }

    private async Task ShowDropdown()
    {
        ShowSuggestions = true;
        await RefreshSuggestions();
    }

    private async Task RefreshSuggestions()
    {
        DebounceCts?.Cancel();
        DebounceCts = new CancellationTokenSource();
        var token = DebounceCts.Token;

        try
        {
            await Task.Delay(200, token);
            if (SearchFunction is not null)
            {
                FilteredItems = await SearchFunction(SearchText);
            }
            //if (!string.IsNullOrWhiteSpace(SearchText) && SearchFunction is not null)
            //{
            //    FilteredItems = await SearchFunction(SearchText);
            //}
            //else
            //{
            //    FilteredItems = new();
            //}
        }
        catch (TaskCanceledException) { }

        ShowSuggestions = true;
        StateHasChanged();
    }

    private async Task SelectItem(TItem item)
    {
        SelectedItem = item;
        SearchText = ItemSelector(item);
        ShowSuggestions = false;
        await SelectedChanged.InvokeAsync(item);
    }

    private async Task ClearSelection()
    {
        SelectedItem = default;
        SearchText = string.Empty;
        FilteredItems = new();
        ShowSuggestions = false;
        await SelectedChanged.InvokeAsync(default);
    }

    private async Task OnInput(ChangeEventArgs e)
    {
        SearchText = e.Value?.ToString() ?? string.Empty;
        await RefreshSuggestions();
    }
}
