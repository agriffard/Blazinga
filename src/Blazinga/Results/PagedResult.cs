namespace Blazinga.Results;
public class PagedResult<T>
{
    public PagedResult()
    {
    }

    public PagedResult(List<T> data, int pageNumber, int pageSize, int totalCount) : this()
    {
        Data = data ?? new List<T>();
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public List<T> Data { get; set; } = new List<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}
