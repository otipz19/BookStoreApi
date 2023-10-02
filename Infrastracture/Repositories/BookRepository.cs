using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Pagination;
using Infrastructure.Data;
using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context)
            :base(context) { }

        public async Task<IPaginatedList<Book>> GetAll(
            int pageIndex,
            int pageSize,
            string? titleSearch = null,
            string? authorSearch = null,
            string? genreSearch = null)
        {
            var query = _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .AsQueryable();

            if (!string.IsNullOrEmpty(titleSearch))
            {
                query = query.Where(b => b.Title.Contains(titleSearch));
            }

            if (!string.IsNullOrEmpty(authorSearch))
            {
                query = query.Where(b => b.Author!.Name.Contains(authorSearch));
            }

            if (!string.IsNullOrEmpty(genreSearch))
            {
                query = query.Where(b => b.Genre!.Name.Contains(genreSearch));
            }

            return await PaginatedList<Book>.CreateAsync(query, pageIndex, pageSize);
        }
    }
}