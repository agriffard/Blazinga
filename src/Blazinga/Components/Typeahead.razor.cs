namespace Blazinga.Components;
public partial class Typeahead<TItem>
{
    [Parameter] public Func<TItem, string> ItemSelector { get; set; } = item => item?.ToString() ?? string.Empty;
    [Parameter] public RenderFragment<TItem>? ItemTemplate { get; set; }
    [Parameter] public string Placeholder { get; set; } = "Search...";
    [Parameter] public Func<string, Task<List<TItem>>> SearchFunction { get; set; } = default!;
    [Parameter] public EventCallback<List<TItem>> SelectedChanged { get; set; }
    [Parameter] public List<TItem> SelectedItems { get; set; } = new();

    private List<TItem> FilteredItems { get; set; } = new();
    private string SearchText { get; set; } = string.Empty;
    private bool ShowSuggestions { get; set; } = false;
    private CancellationTokenSource? DebounceCts;
    private ElementReference inputRef;

    private async Task ShowDropdown()
    {
        ShowSuggestions = true;
        await RefreshSuggestions();
    }

    private async Task OnInput(ChangeEventArgs e)
    {
        SearchText = e.Value?.ToString() ?? string.Empty;
        await RefreshSuggestions();
    }

    private async Task RefreshSuggestions()
    {
        DebounceCts?.Cancel();
        DebounceCts = new CancellationTokenSource();
        var token = DebounceCts.Token;

        try
        {
            await Task.Delay(250, token);
            if (SearchFunction is not null)
            {
                FilteredItems = await SearchFunction(SearchText);
                FilteredItems = FilteredItems
                    .Where(item => !SelectedItems.Any(sel => ItemSelector(sel) == ItemSelector(item)))
                    .ToList();
            }
            //if (!string.IsNullOrWhiteSpace(SearchText) && SearchFunction is not null)
            //{
            //    FilteredItems = await SearchFunction(SearchText);
            //    FilteredItems = FilteredItems
            //        .Where(item => !SelectedItems.Any(sel => ItemSelector(sel) == ItemSelector(item)))
            //        .ToList();
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
        if (!SelectedItems.Contains(item))
        {
            SelectedItems.Add(item);
            await SelectedChanged.InvokeAsync(SelectedItems);
        }

        SearchText = string.Empty;
        FilteredItems.Clear();
        ShowSuggestions = false;
        await inputRef.FocusAsync();
    }

    private async Task RemoveItem(TItem item)
    {
        SelectedItems.Remove(item);
        await SelectedChanged.InvokeAsync(SelectedItems);
    }

    private void HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Backspace" && string.IsNullOrEmpty(SearchText) && SelectedItems.Any())
        {
            SelectedItems.RemoveAt(SelectedItems.Count - 1);
            SelectedChanged.InvokeAsync(SelectedItems);
        }
    }
}
