namespace Blazinga.Components;
public partial class DropdownSelect<TItem, TValue>
{
    [Parameter] public List<TItem> Items { get; set; } = new List<TItem>();
    [Parameter] public TValue Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public Expression<Func<TItem, TValue>> ValueSelector { get; set; }
    [Parameter] public Expression<Func<TItem, string>> TextSelector { get; set; }
    [Parameter] public int Height { get; set; } = 200;
    [Parameter] public bool AllowSearch { get; set; }
    [Parameter] public string Placeholder { get; set; }
    [Parameter] public string SearchPlaceholder { get; set; }
    [Parameter] public string NoResultsText { get; set; } // = SharedResource.NoResults;
    [Parameter] public string CssClass { get; set; } = string.Empty;
    [Parameter] public string ItemCssClass { get; set; } = string.Empty;

    private bool IsDropdownVisible { get; set; }
    private string SearchText { get; set; } = string.Empty;

    private List<TItem> FilteredItems { get; set; } = new();

    private TItem SelectedItem => Items.FirstOrDefault(item => EqualityComparer<TValue>.Default.Equals(ValueSelector.Compile().Invoke(item), Value));

    private void ToggleDropdown()
    {
        IsDropdownVisible = !IsDropdownVisible;
        if (IsDropdownVisible)
        {
            FilterItems();
        }
    }

    private async Task SelectItem(TItem item)
    {
        Value = ValueSelector.Compile().Invoke(item);
        await ValueChanged.InvokeAsync(Value);
        IsDropdownVisible = false;
    }

    private async Task ResetValue()
    {
        Value = default;
        await ValueChanged.InvokeAsync(Value);
        SearchText = string.Empty;
        FilterItems();
        IsDropdownVisible = false;
    }

    private void OnSearchTextInput(ChangeEventArgs e)
    {
        SearchText = e.Value?.ToString() ?? string.Empty;
        FilterItems();
        IsDropdownVisible = true;
    }

    private void FilterItems()
    {
        FilteredItems = Items
            .Where(item => string.IsNullOrWhiteSpace(SearchText) ||
                           TextSelector.Compile().Invoke(item).Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        FilterItems();
    }
}
