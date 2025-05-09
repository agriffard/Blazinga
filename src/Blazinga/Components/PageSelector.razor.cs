namespace Blazinga.Components;
public partial class PageSelector
{
    [Parameter] public int TotalCount { get; set; }
    [Parameter] public int PageSize { get; set; }
    [Parameter] public int PageNumber { get; set; } = 1;
    [Parameter] public EventCallback PageChanged { get; set; }

    private int totalPageCount => (int)Math.Ceiling(((decimal)TotalCount / PageSize));

    public void OnChanged() => StateHasChanged();

    public void ResetPageNumber()
    {
        PageNumber = 1;
    }

    private async Task NextPage()
    {
        PageNumber++;
        if (PageNumber > totalPageCount)
        {
            PageNumber--;
        }
        await PageChanged.InvokeAsync(PageNumber);
    }

    private async Task PreviousPage()
    {
        PageNumber--;
        if (PageNumber < 1)
        {
            PageNumber++;
        }
        await PageChanged.InvokeAsync(PageNumber);
    }

    private int GetFirstItemIndex()
    {
        return TotalCount == 0 ? 0 : (PageNumber - 1) * PageSize + 1;
    }

    private int GetLastItemIndex()
    {
        var lastPossibleItem = PageNumber * PageSize;
        return lastPossibleItem > TotalCount ? TotalCount : lastPossibleItem;
    }
}
