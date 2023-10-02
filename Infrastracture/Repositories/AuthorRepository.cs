using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Pagination;
using Infrastructure.Data;
using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IPaginatedList<Author>> GetAllAuthors(int pageIndex, int pageSize)
        {
            return await PaginatedList<Author>.CreateAsync(_context.Authors.AsNoTracking(), pageIndex, pageSize);
        }

        public async Task<Author> AddAuthor(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> GetAuthorById(int id)
        {
            var author = await _context.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (author == null)
            {
                throw new AuthorNotFoundException(id);
            }

            return author;
        }

        public async Task UpdateAuthor(Author author)
        {
            var existingAuthor = await _context.Authors.FindAsync(author.Id);

            if (existingAuthor == null)
            {
                throw new AuthorNotFoundException(author.Id);
            }

            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                throw new AuthorNotFoundException(id);
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}