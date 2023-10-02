using Domain.Entities;
using Domain.Pagination;

namespace Domain.Contracts.Repositories;

public interface IGenreRepository
{
    Task<IPaginatedList<Genre>> GetAll(int pageIndex, int pageSize);
    Task<Genre> GetById(int id);
    Task<Genre> Add(Genre entity);
    Task Update(Genre entity);
    Task Delete(int id);
}