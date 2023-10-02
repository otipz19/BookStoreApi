using Domain.Entities;
using Domain.Pagination;

namespace Domain.Contracts.Repositories;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<IPaginatedList<Book>> GetAll(
        int pageIndex,
        int pageSize,
        string? titleSearch = null,
        string? authorSearch = null,
        string? genreSearch = null);
}