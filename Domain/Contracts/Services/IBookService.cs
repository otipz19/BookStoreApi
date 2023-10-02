using Domain.Entities;
using Domain.Pagination;

namespace Domain.Contracts.Services
{
    public interface IBookService
    {
        Task<IPaginatedList<Book>> GetAllBooks(
            int pageIndex,
            int pageSize,
            string? titleSearch = null,
            string? authorSearch = null,
            string? genreSearch = null);
        Task<Book> GetBookById(int id);
        Task<Book> AddBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
    }
}