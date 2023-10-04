using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Pagination;
using Infrastructure.Pagination;

namespace Service.Tests.Mocks
{
    public class GenreRepositoryMock : IGenreRepository
    {
        private readonly List<Genre> _genres = new();

        public Task<IPaginatedList<Genre>> GetAll(int pageIndex, int pageSize)
        {
            var genres = _genres.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return Task.FromResult<IPaginatedList<Genre>>(new PaginatedList<Genre>(genres, _genres.Count, pageIndex, pageSize));
        }

        public Task<Genre> GetById(int id)
        {
            var genre = _genres.FirstOrDefault(g => g.Id == id);
            return Task.FromResult(genre);
        }

        public Task<Genre> Add(Genre entity)
        {
            var id = _genres.Any() ? _genres.Max(g => g.Id) + 1 : 1;
            entity.Id = id;
            _genres.Add(entity);
            return Task.FromResult(entity);
        }

        public Task Update(Genre entity)
        {
            var index = _genres.FindIndex(g => g.Id == entity.Id);
            if (index >= 0)
            {
                _genres[index] = entity;
            }

            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            _genres.RemoveAll(g => g.Id == id);
            return Task.CompletedTask;
        }
    }
}