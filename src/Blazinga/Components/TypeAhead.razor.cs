namespace Blazinga.Components;
public partial class TypeAhead<TItem, TValue>
{
    [Parameter] public Func<string, CancellationToken, Task<PagedResult<TItem>>> SearchFunction { get; set; }
    [Parameter] public EventCallback OnReset { get; set; }
    [Parameter] public string NoResultsText { get; set; } // = SharedResource.NoResults;
    [Parameter] public string Placeholder { get; set; }
    [Parameter] public string CssClass { get; set; }
    [Parameter] public int Height { get; set; } = 150;
    [Parameter] public TValue Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public EventCallback<string> TextChanged { get; set; }
    [Parameter] public EventCallback<TItem> SelectedItemChanged { get; set; }
    [Parameter] public Expression<Func<TItem, TValue>> ValueSelector { get; set; }
    [Parameter] public Expression<Func<TItem, string>> TextSelector { get; set; }
    [Parameter] public string InitialText { get; set; }
    public string Text { get; set; }
    public TItem SelectedItem { get; set; }
    private PagedResult<TItem> FilteredItems { get; set; } = new();
    private bool IsDropdownVisible { get; set; }

    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    protected override async Task OnInitializedAsync()
    {
        Text = InitialText;
    }

    private async Task OnTextChanged(ChangeEventArgs e)
    {
        Text = e.Value?.ToString();
        await TextChanged.InvokeAsync(Text);

        if (!IsDropdownVisible)
        {
            IsDropdownVisible = true;
        }

        try
        {
            FilteredItems = await GetFilteredItems(Text);
            IsDropdownVisible = FilteredItems.Data.Count > 0;
        }
        catch (InvalidOperationException ioex)
        {
            // Even if we use CancellationToken, it can happen sometimes.
            // System.InvalidOperationException: A second operation was started on this context instance before a previous operation completed. This is usually caused by different threads concurrently using the same instance of DbContext. For more information on how to avoid threading issues with DbContext
        }
    }

    private void HideDropdown()
    {
        IsDropdownVisible = false;
    }

    private async Task<PagedResult<TItem>> GetFilteredItems(string text)
    {
        // Cancel the previous search operation
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        return await SearchFunction(text, _cancellationTokenSource.Token);
    }

    private async Task ToggleDropdown()
    {
        //if opening for the first time, fetch datas
        if (!IsDropdownVisible && string.IsNullOrEmpty(Text) && FilteredItems.TotalCount == 0)
        {
            FilteredItems = await GetFilteredItems(Text);
        }

        IsDropdownVisible = !IsDropdownVisible;
    }

    private void OnItemKeyDown(KeyboardEventArgs e, TItem item)
    {
        if (e.Key == "Enter")
        {
            // Select the item when Enter is pressed
            SelectItem(item);
        }
    }

    private async Task ResetSelection()
    {
        //If the string is not already empty get items
        var newText = string.Empty;
        if (Text != newText)
        {
            FilteredItems = await GetFilteredItems(newText);
        }
        Text = newText;

        SelectedItem = default(TItem);
        Value = default(TValue);
        IsDropdownVisible = false;
        await OnReset.InvokeAsync();
        await SelectedItemChanged.InvokeAsync(SelectedItem);
        await ValueChanged.InvokeAsync(Value);
        await TextChanged.InvokeAsync(Text);
    }

    private async Task SelectItem(TItem item)
    {
        var newText = TextSelector.Compile().Invoke(item);
        if (Text != newText)
        {
            FilteredItems = await GetFilteredItems(newText);
        }
        Text = newText;

        SelectedItem = item;
        Value = ValueSelector.Compile().Invoke(item);
        IsDropdownVisible = false;
        await SelectedItemChanged.InvokeAsync(SelectedItem);
        await ValueChanged.InvokeAsync(Value);
        await TextChanged.InvokeAsync(Text);
    }
}
