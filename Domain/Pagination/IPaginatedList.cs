namespace Domain.Pagination
{
    public interface IPaginatedList<T>
    {
        int PageIndex { get; }
        int TotalPages { get; }
        List<T> Items { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}