using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Pagination;
using Infrastructure.Data;
using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IPaginatedList<Book>> GetAllBooks(
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

        public async Task<Book> GetBookById(int id)
        {
            var book = await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                throw new BookNotFoundException(id);
            }

            return book;
        }

        public async Task UpdateBook(Book book)
        {
            var existingBook = await _context.Books.FindAsync(book.Id);

            if (existingBook == null)
            {
                throw new BookNotFoundException(book.Id);
            }

            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                throw new BookNotFoundException(id);
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> AddBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}