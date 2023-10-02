using Domain.Entities;
using Domain.Pagination;

namespace Domain.Contracts.Repositories;

public interface IGenreRepository
{
    Task<IPaginatedList<Genre>> GetAllGenres(int pageIndex, int pageSize);
    Genre GetGenreById(int id);
    void AddGenre(Genre genre);
    void UpdateGenre(Genre genre);
    void DeleteGenre(int id);
}