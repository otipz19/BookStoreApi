using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Pagination;
using Infrastructure.Pagination;

namespace Service.Tests.Mocks
{
    public class BookRepositoryMock : IBookRepository
    {
        private readonly List<Book> _books = new();

        public Task<IPaginatedList<Book>> GetAll(
            int pageIndex,
            int pageSize,
            string? titleSearch = null,
            string? authorSearch = null,
            string? genreSearch = null)
        {
            var books = _books.Where(b =>
                (string.IsNullOrEmpty(titleSearch) || b.Title.Contains(titleSearch)) &&
                (string.IsNullOrEmpty(authorSearch) || b.Author.Name.Contains(authorSearch)) &&
                (string.IsNullOrEmpty(genreSearch) || b.Genre.Name.Contains(genreSearch)))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalCount = _books.Count(b =>
                (string.IsNullOrEmpty(titleSearch) || b.Title.Contains(titleSearch)) &&
                (string.IsNullOrEmpty(authorSearch) || b.Author.Name.Contains(authorSearch)) &&
                (string.IsNullOrEmpty(genreSearch) || b.Genre.Name.Contains(genreSearch)));

            return Task.FromResult<IPaginatedList<Book>>(new PaginatedList<Book>(books, totalCount, pageIndex, pageSize));
        }

        public Task<Book> GetById(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            return Task.FromResult(book);
        }

        public Task<Book> Add(Book entity)
        {
            var id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
            entity.Id = id;
            _books.Add(entity);
            return Task.FromResult(entity);
        }

        public Task Update(Book entity)
        {
            var index = _books.FindIndex(b => b.Id == entity.Id);
            if (index >= 0)
            {
                _books[index] = entity;
            }

            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            _books.RemoveAll(b => b.Id == id);
            return Task.CompletedTask;
        }
    }
}