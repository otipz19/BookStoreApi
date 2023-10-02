using Domain.Entities;
using Domain.Pagination;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<IPaginatedList<TEntity>> GetAll(int pageIndex, int pageSize);
    Task<TEntity> GetById(int id);
    Task<TEntity> Add(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(int id);
}