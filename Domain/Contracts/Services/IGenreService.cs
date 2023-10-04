using Domain.Dto;
using Domain.Entities;
using Domain.Pagination;

namespace Domain.Contracts.Services
{
    public interface IGenreService
    {
        Task<IPaginatedList<GenreDto>> GetAll(int pageIndex, int pageSize);
        Task<GenreDto> GetById(int id);
        Task<GenreDto> Add(GenreDto genreDto);
        Task Update(GenreDto genreDto);
        Task Delete(int id);
    }
}