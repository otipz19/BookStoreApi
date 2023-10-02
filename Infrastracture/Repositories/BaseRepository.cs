using Domain.Entities;
using Domain.Exceptions;
using Domain.Pagination;
using Infrastructure.Data;
using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IPaginatedList<TEntity>> GetAll(int pageIndex, int pageSize)
        {
            return await PaginatedList<TEntity>.CreateAsync(_context.Set<TEntity>().AsNoTracking(), pageIndex, pageSize);
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            var entity = await _context.Set<TEntity>().
                AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                throw new NotFoundException<TEntity>(id);
            }

            return entity;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task Update(TEntity entity)
        {
            var existingEntity = await _context.Set<TEntity>().FindAsync(entity.Id);

            if (existingEntity == null)
            {
                throw new NotFoundException<TEntity>(entity.Id);
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException<TEntity>(id);
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}