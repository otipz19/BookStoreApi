using Domain.Entities;
using Domain.Pagination;

namespace Domain.Contracts.Repositories;

public interface IAuthorRepository
{
    Task<IPaginatedList<Author>> GetAllAuthors(int pageIndex, int pageSize);
    Author GetAuthorById(int id);
    void AddAuthor(Author author);
    void UpdateAuthor(Author author);
    void DeleteAuthor(int id);
}