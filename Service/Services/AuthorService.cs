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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AuthorDto> _validator;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper, IValidator<AuthorDto> validator)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IPaginatedList<AuthorDto>> GetAll(int pageIndex, int pageSize)
        {
            var paginatedEntities = await _authorRepository.GetAll(pageIndex, pageSize);
            var dtos = paginatedEntities.Select(_mapper.Map<Author, AuthorDto>);
            return paginatedEntities.Select(dtos);
        }

        public async Task<AuthorDto> GetById(int id)
        {
            var author = await _authorRepository.GetById(id);
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> Add(AuthorDto authorDto)
        {
            var validationResult = await _validator.ValidateAsync(authorDto);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException<AuthorDto>("AuthorDto is not valid.", authorDto);
            }

            var author = _mapper.Map<Author>(authorDto);
            var addedAuthor = await _authorRepository.Add(author);
            return _mapper.Map<AuthorDto>(addedAuthor);
        }

        public async Task Update(AuthorDto authorDto)
        {
            var validationResult = await _validator.ValidateAsync(authorDto);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException<AuthorDto>("AuthorDto is not valid.", authorDto);
            }

            var author = _mapper.Map<Author>(authorDto);
            await _authorRepository.Update(author);
        }

        public async Task Delete(int id)
        {
            await _authorRepository.Delete(id);
        }
    }
}