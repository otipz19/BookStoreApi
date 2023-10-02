using Domain.Entities;
using Domain.Pagination;

namespace Domain.Contracts.Repositories;

public interface IAuthorRepository
{
    Task<IPaginatedList<Author>> GetAllAuthors(int pageIndex, int pageSize);
    Task<Author> GetAuthorById(int id);
    Task<Author> AddAuthor(Author author);
    Task UpdateAuthor(Author author);
    Task DeleteAuthor(int id);
}