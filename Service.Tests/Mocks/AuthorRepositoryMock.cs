using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Pagination;
using Infrastructure.Pagination;

namespace Service.Tests.Mocks
{
    public class AuthorRepositoryMock : IAuthorRepository
    {
        private readonly List<Author> _authors = new();

        public Task<IPaginatedList<Author>> GetAll(int pageIndex, int pageSize)
        {
            var authors = _authors.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return Task.FromResult<IPaginatedList<Author>>(new PaginatedList<Author>(authors, _authors.Count, pageIndex, pageSize));
        }

        public Task<Author> GetById(int id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(author);
        }

        public Task<Author> Add(Author entity)
        {
            var id = _authors.Any() ? _authors.Max(a => a.Id) + 1 : 1;
            entity.Id = id;
            _authors.Add(entity);
            return Task.FromResult(entity);
        }

        public Task Update(Author entity)
        {
            var index = _authors.FindIndex(a => a.Id == entity.Id);
            if (index >= 0)
            {
                _authors[index] = entity;
            }

            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            _authors.RemoveAll(a => a.Id == id);
            return Task.CompletedTask;
        }
    }
}