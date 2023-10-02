using Domain.Entities;
using Domain.Pagination;

namespace Domain.Contracts.Repositories;

public interface IAuthorRepository
{
    Task<IPaginatedList<Author>> GetAll(int pageIndex, int pageSize);
    Task<Author> GetById(int id);
    Task<Author> Add(Author entity);
    Task Update(Author entity);
    Task Delete(int id);
}