using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Pagination;
using Infrastructure.Data;
using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IPaginatedList<Genre>> GetAllGenres(int pageIndex, int pageSize)
        {
            return await PaginatedList<Genre>.CreateAsync(_context.Genres.AsNoTracking(), pageIndex, pageSize);
        }

        public async Task<Genre> GetGenreById(int id)
        {
            var genre = await _context.Genres
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (genre == null)
            {
                throw new GenreNotFoundException(id);
            }

            return genre;
        }

        public async Task<Genre> AddGenre(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task UpdateGenre(Genre genre)
        {
            var existingGenre = await _context.Genres.FindAsync(genre.Id);

            if (existingGenre == null)
            {
                throw new GenreNotFoundException(genre.Id);
            }

            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                throw new GenreNotFoundException(id);
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}