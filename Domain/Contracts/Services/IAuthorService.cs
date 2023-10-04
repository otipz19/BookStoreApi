using Domain.Dto;
using Domain.Pagination;

namespace Domain.Contracts.Services
{
    public interface IAuthorService
    {
        Task<IPaginatedList<AuthorDto>> GetAll(int pageIndex, int pageSize);
        Task<AuthorDto> GetById(int id);
        Task<AuthorDto> Add(AuthorDto authorDto);
        Task Update(AuthorDto authorDto);
        Task Delete(int id);
    }
}