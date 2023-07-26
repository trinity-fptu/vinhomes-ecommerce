namespace Application.Commons;

public class Pagination<T>
{
    private const int MaxPageSize = 100;
    private readonly int _total;
    private readonly int _pageIndex;
    private readonly int _pageSize;

    public Pagination(IEnumerable<T> items, int total, int pageIndex, int pageSize)
    {
        _total = total;
        _pageIndex = Math.Max(pageIndex, 0);
        _pageSize = Math.Clamp(pageSize, 1, MaxPageSize); 
        Items = items.ToList();
    }

    public int TotalPages => (_total + _pageSize - 1) / _pageSize;

    public bool HasNextPage => _pageIndex + 1 < TotalPages;

    public bool HasPreviousPage => _pageIndex > 0;

    public IReadOnlyList<T> Items { get; }
}

