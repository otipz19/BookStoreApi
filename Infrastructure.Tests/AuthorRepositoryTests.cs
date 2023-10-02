using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Infrastructure.Tests
{
    public class AuthorRepositoryTests
    {
        private AppDbContext _context;
        private AuthorRepository _authorRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            _authorRepository = new AuthorRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task AddAuthor_Author_ReturnsNewAuthor()
        {
            // Arrange
            var author = new Author { Name = "John Doe" };

            // Act
            var result = await _authorRepository.Add(author);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Name, Is.EqualTo(author.Name));
        }

        [Test]
        public async Task GetAuthorById_AuthorExists_ReturnsExpectedAuthor()
        {
            // Arrange
            var author = new Author { Name = "John Doe" };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            // Act
            var result = await _authorRepository.GetById(author.Id);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(author.Id));
        }

        [Test]
        public void GetAuthorById_AuthorDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistingId = 999;

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException<Author>>(() => _authorRepository.GetById(nonExistingId));
        }

        [Test]
        public async Task UpdateAuthor_AuthorExists_UpdatesName()
        {
            // Arrange
            var author = new Author { Name = "John Doe" };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            author.Name = "Jane Doe";

            // Act
            await _authorRepository.Update(author);

            // Assert
            var result = await _authorRepository.GetById(author.Id);
            Assert.That(result.Name, Is.EqualTo(author.Name));
        }

        [Test]
        public void UpdateAuthor_AuthorDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistingAuthor = new Author { Id = 999, Name = "Non-existing" };

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException<Author>>(() => _authorRepository.Update(nonExistingAuthor));
        }

        [Test]
        public async Task DeleteAuthor_AuthorExists_DeletesAuthor()
        {
            // Arrange
            var author = new Author { Name = "John Doe" };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            // Act
            await _authorRepository.Delete(author.Id);

            // Assert
            var result = await _context.Authors.FindAsync(author.Id);
            Assert.IsNull(result);
        }

        [Test]
        public void DeleteAuthor_AuthorDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistingId = 999;

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException<Author>>(() => _authorRepository.Delete(nonExistingId));
        }
    }
}