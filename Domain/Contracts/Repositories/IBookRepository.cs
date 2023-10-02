using Domain.Entities;
using Domain.Pagination;

namespace Domain.Contracts.Repositories;

public interface IBookRepository
{
    Task<IPaginatedList<Book>> GetAllBooks(
        int pageIndex,
        int pageSize,
        string? titleSearch = null,
        string? authorSearch = null,
        string? genreSearch = null);
    Book GetBookById(int id);
    void AddBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(int id);
}