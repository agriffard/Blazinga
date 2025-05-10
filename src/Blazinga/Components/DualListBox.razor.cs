namespace Blazinga.Components;
public partial class DualListBox<TItem>
{
    [Parameter] public List<TItem> AvailableItems { get; set; } = new();
    [Parameter] public List<TItem> SelectedItems { get; set; } = new();
    [Parameter] public EventCallback<List<TItem>> SelectedItemsChanged { get; set; }
    [Parameter] public Func<TItem, string> KeySelector { get; set; } = item => item?.ToString() ?? "";
    [Parameter] public Func<TItem, string> TextSelector { get; set; } = item => item?.ToString() ?? string.Empty;
    [Parameter] public int Height { get; set; } = 200;

    private HashSet<string> selectedAvailableItemKeys = new();
    private HashSet<string> selectedSelectedItemKeys = new();
    private string GetItemKey(TItem item) => KeySelector(item);

    private void OnAvailableChanged(ChangeEventArgs e)
    {
        selectedAvailableItemKeys = ParseSelectedKeys(e.Value);
    }

    private void OnSelectedChanged(ChangeEventArgs e)
    {
        selectedSelectedItemKeys = ParseSelectedKeys(e.Value);
    }

    private HashSet<string> ParseSelectedKeys(object? value)
    {
        var keys = new HashSet<string>();
        if (value is string s)
        {
            keys.Add(s);
        }
        else if (value is string[] array)
        {
            foreach (var v in array)
                keys.Add(v);
        }
        return keys;
    }

    private void MoveToSelected()
    {
        var itemsToAdd = AvailableItems
            .Where(item => selectedAvailableItemKeys.Contains(GetItemKey(item)))
            .ToList();

        foreach (var item in itemsToAdd)
        {
            if (!SelectedItems.Contains(item))
            {
                SelectedItems.Add(item);
            }
        }

        selectedAvailableItemKeys.Clear();
        SelectedItemsChanged.InvokeAsync(SelectedItems);
    }

    private void MoveToAvailable()
    {
        var itemsToRemove = SelectedItems
            .Where(item => selectedSelectedItemKeys.Contains(GetItemKey(item)))
            .ToList();

        foreach (var item in itemsToRemove)
        {
            SelectedItems.Remove(item);
        }

        selectedSelectedItemKeys.Clear();
        SelectedItemsChanged.InvokeAsync(SelectedItems);
    }
}
