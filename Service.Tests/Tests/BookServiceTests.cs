using AutoMapper;
using AutoMapper.Execution;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Dto;
using Domain.Entities;
using Domain.Exceptions;
using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Service.Services;
using Service.Tests.Mocks;
using Service.Validators;

namespace Service.Tests
{
    [TestFixture]
    public class BookServiceTests
    {
        private IBookService _bookService;
        private IValidator<CreateBookDto> _validator;
        private IAuthorRepository _authorRepo;
        private IGenreRepository _genreRepo;

        [SetUp]
        public void SetUp()
        {
            _genreRepo = new GenreRepositoryMock();
            _authorRepo = new AuthorRepositoryMock();
            var bookRepo = new BookRepositoryMock();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.ConstructServicesUsing(ObjectFactory.CreateInstance);

                cfg.CreateMap<CreateBookDto, Book>();

                cfg.CreateMap<Book, GetBookDto>()
                    .ForMember(dest => dest.AuthorName,
                        opt => opt.MapFrom(src => src.Author.Name))
                    .ForMember(dest => dest.GenreName,
                        opt => opt.MapFrom(src => src.Genre.Name));
            });
            var mapper = new Mapper(config);

            _validator = new CreateBookDtoValidator();

            _bookService = new BookService(bookRepo, _authorRepo, _genreRepo, mapper, _validator);
        }

        [Test]
        public async Task Add_ShouldThrowException_WhenTitleIsEmpty()
        {
            var author = new Author() { Id = 1, Name = "Name" };
            await _authorRepo.Add(author);
            var genre = new Genre() { Id = 1, Name = "Name" };
            await _genreRepo.Add(genre);
            var bookDto = new CreateBookDto("", 5, 1, 1, 1);

            var res = _validator.TestValidate(bookDto);

            res.ShouldHaveValidationErrorFor(b => b.Title);
            Assert.ThrowsAsync<BadRequestException<CreateBookDto>>(() => _bookService.Add(bookDto));
        }

        [Test]
        public async Task Add_ShouldThrowException_WhenAuthorNotExists()
        {
            var genre = new Genre() { Id = 1, Name = "Name" };
            await _genreRepo.Add(genre);
            var bookDto = new CreateBookDto("Book test", 5, 1, 999, 1);

            Assert.ThrowsAsync<BadRequestException<CreateBookDto>>(() => _bookService.Add(bookDto));
        }

        [Test]
        public async Task Add_ShouldThrowException_WhenGenreNotExists()
        {
            var bookDto = new CreateBookDto("Book Test", 5, 1, 1, 999);
            Assert.ThrowsAsync<BadRequestException<CreateBookDto>>(() => _bookService.Add(bookDto));
        }

        [Test]
        public async Task Add_ShouldPass_WhenAuthorAndGenreExists()
        {
            var author = new Author() { Id = 1, Name = "Name" };
            await _authorRepo.Add(author);
            var genre = new Genre() { Id = 1, Name = "Name" };
            await _genreRepo.Add(genre);
            var bookDto = new CreateBookDto("Book Test", 5, 1, 1, 1);

            var newBookDto = await _bookService.Add(bookDto);

            Assert.AreEqual(bookDto.Title, newBookDto.Title);
            Assert.AreEqual(bookDto.AuthorId, newBookDto.AuthorId);
            Assert.AreEqual(bookDto.GenreId, newBookDto.GenreId);
        }
    }
}