using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Dto;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Pagination;
using FluentValidation;

namespace Service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookDto> _validator;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository, IMapper mapper, IValidator<CreateBookDto> validator)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IPaginatedList<GetBookDto>> GetAll(
            int pageIndex,
            int pageSize,
            string? titleSearch = null,
            string? authorSearch = null,
            string? genreSearch = null)
        {
            var paginatedBooks = await _bookRepository.GetAll(pageIndex, pageSize, titleSearch, authorSearch, genreSearch);
            var dtos = paginatedBooks.Select(_mapper.Map<GetBookDto>);
            return paginatedBooks.Select(dtos);
        }

        public async Task<GetBookDto> GetById(int id)
        {
            var book = await _bookRepository.GetById(id);
            return _mapper.Map<GetBookDto>(book);
        }

        public async Task<GetBookDto> Add(CreateBookDto createBookDto)
        {
            await ValidateBookDto(createBookDto);

            var book = _mapper.Map<Book>(createBookDto);
            var addedBook = await _bookRepository.Add(book);
            return _mapper.Map<GetBookDto>(addedBook);
        }

        public async Task Update(CreateBookDto updateBookDto, int id)
        {
            await ValidateBookDto(updateBookDto);

            var book = _mapper.Map<Book>(updateBookDto);
            book.Id = id;
            await _bookRepository.Update(book);
        }

        public Task Delete(int id) => _bookRepository.Delete(id);

        private async Task ValidateBookDto(CreateBookDto bookDto)
        {
            var validationResult = await _validator.ValidateAsync(bookDto);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException<CreateBookDto>("GenreDto is not valid.", bookDto);
            }

            var author = await _authorRepository.GetById(bookDto.AuthorId);
            if (author == null)
            {
                throw new BadRequestException<CreateBookDto>("Author does not exist for the given AuthorId.", bookDto);
            }

            var genre = await _genreRepository.GetById(bookDto.GenreId);
            if (genre == null)
            {
                throw new BadRequestException<CreateBookDto>("Genre does not exist for the given GenreId.", bookDto);
            }
        }
    }
}