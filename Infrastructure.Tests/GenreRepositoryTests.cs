using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Infrastructure.Tests
{
    public class GenreRepositoryTests
    {
        private AppDbContext _context;
        private GenreRepository _genreRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            _genreRepository = new GenreRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task Add_Genre_ReturnsNewGenre()
        {
            // Arrange
            var genre = new Genre { Name = "Test" };

            // Act
            var result = await _genreRepository.Add(genre);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Name, Is.EqualTo(genre.Name));
        }

        [Test]
        public async Task GetById_GenreExists_ReturnsExpectedGenre()
        {
            // Arrange
            var genre = new Genre { Name = "Test" };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            // Act
            var result = await _genreRepository.GetById(genre.Id);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(genre.Id));
        }

        [Test]
        public void GetById_GenreDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistingId = 999;

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException<Genre>>(() => _genreRepository.GetById(nonExistingId));
        }

        [Test]
        public async Task Update_GenreExists_UpdatesName()
        {
            // Arrange
            var genre = new Genre { Name = "Test" };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            genre.Name = "Updated";

            // Act
            await _genreRepository.Update(genre);

            // Assert
            var result = await _genreRepository.GetById(genre.Id);
            Assert.That(result.Name, Is.EqualTo("Updated"));
        }

        [Test]
        public void Update_GenreDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistingGenre = new Genre { Id = 999, Name = "Non-existing" };

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException<Genre>>(() => _genreRepository.Update(nonExistingGenre));
        }

        [Test]
        public async Task Delete_GenreExists_DeletesGenre()
        {
            // Arrange
            var genre = new Genre { Name = "Test" };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            // Act
            await _genreRepository.Delete(genre.Id);

            // Assert
            var result = await _context.Genres.FindAsync(genre.Id);
            Assert.IsNull(result);
        }

        [Test]
        public void Delete_GenreDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistingId = 999;

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException<Genre>>(() => _genreRepository.Delete(nonExistingId));
        }
    }
}