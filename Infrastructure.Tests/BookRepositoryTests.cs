using System;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.Repositories
{
    public class BookRepositoryTests
    {
        private AppDbContext _context;
        private BookRepository _bookRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            _bookRepository = new BookRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task Add_Book_ReturnsNewBook()
        {
            // Arrange
            var book = new Book { Title = "Test Book", AuthorId = 1, GenreId = 1 };

            // Act
            var result = await _bookRepository.Add(book);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Title, Is.EqualTo(book.Title));
        }

        [Test]
        public async Task GetById_BookExists_ReturnsExpectedBook()
        {
            // Arrange
            var book = new Book { Title = "Test Book", AuthorId = 1, GenreId = 1 };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Act
            var result = await _bookRepository.GetById(book.Id);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(book.Id));
        }

        [Test]
        public void GetById_BookDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistingId = 999;

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException<Book>>(() => _bookRepository.GetById(nonExistingId));
        }

        [Test]
        public async Task Update_BookExists_UpdatesTitle()
        {
            // Arrange
            var book = new Book { Title = "Test Book", AuthorId = 1, GenreId = 1 };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            book.Title = "Updated Test Book";

            // Act
            await _bookRepository.Update(book);

            // Assert
            var result = await _bookRepository.GetById(book.Id);
            Assert.That(result.Title, Is.EqualTo(book.Title));
        }

        [Test]
        public void Update_BookDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistingBook = new Book { Id = 999, Title = "Non-existing", AuthorId = 1, GenreId = 1 };

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException<Book>>(() => _bookRepository.Update(nonExistingBook));
        }

        [Test]
        public async Task Delete_BookExists_DeletesBook()
        {
            // Arrange
            var book = new Book { Title = "Test Book", AuthorId = 1, GenreId = 1 };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Act
            await _bookRepository.Delete(book.Id);

            // Assert
            var result = await _context.Books.FindAsync(book.Id);
            Assert.IsNull(result);
        }

        [Test]
        public void Delete_BookDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var nonExistingId = 999;

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException<Book>>(() => _bookRepository.Delete(nonExistingId));
        }
    }
}