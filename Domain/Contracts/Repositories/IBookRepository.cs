using Domain.Entities;
using Domain.Pagination;

namespace Domain.Contracts.Repositories;

public interface IBookRepository
{
    Task<IPaginatedList<Book>> GetAll(int pageIndex,
        int pageSize,
        string? titleSearch = null,
        string? authorSearch = null,
        string? genreSearch = null);
    Task<Book> GetById(int id);
    Task<Book> Add(Book entity);
    Task Update(Book entity);
    Task Delete(int id);
}