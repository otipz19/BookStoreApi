using Domain.Dto;
using Domain.Entities;
using Domain.Pagination;

namespace Domain.Contracts.Services
{
    public interface IBookService
    {
        Task<IPaginatedList<GetBookDto>> GetAll(
            int pageIndex,
            int pageSize,
            string? titleSearch = null,
            string? authorSearch = null,
            string? genreSearch = null);
        Task<GetBookDto> GetById(int id);
        Task<GetBookDto> Add(CreateBookDto createBookDto);
        Task Update(CreateBookDto updateBookDto, int id);
        Task Delete(int id);
    }
}