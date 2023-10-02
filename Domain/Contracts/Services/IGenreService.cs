using Domain.Entities;
using Domain.Pagination;

namespace Domain.Contracts.Services
{
    public interface IGenreService
    {
        Task<IPaginatedList<Genre>> GetAllGenres(int pageIndex, int pageSize);
        Task<Genre> GetGenreById(int id);
        Task<Genre> AddGenre(Genre genre);
        Task UpdateGenre(Genre genre);
        Task DeleteGenre(int id);
    }
}