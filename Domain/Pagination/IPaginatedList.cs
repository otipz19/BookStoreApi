namespace Domain.Pagination
{
    public interface IPaginatedList<T> : IList<T>
    {
        int PageIndex { get; }
        int TotalPages { get; }
        List<T> Items { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }

        IPaginatedList<TNew> Select<TNew>(IEnumerable<TNew> collection);
    }
}