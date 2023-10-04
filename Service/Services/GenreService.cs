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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GenreDto> _validator;

        public GenreService(IGenreRepository genreRepository, IMapper mapper, IValidator<GenreDto> validator)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IPaginatedList<GenreDto>> GetAll(int pageIndex, int pageSize)
        {
            var paginatedGenres = await _genreRepository.GetAll(pageIndex, pageSize);
            var dtos = paginatedGenres.Select(_mapper.Map<GenreDto>);
            return paginatedGenres.Select(dtos);
        }

        public async Task<GenreDto> GetById(int id)
        {
            var genre = await _genreRepository.GetById(id);
            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<GenreDto> Add(GenreDto genreDto)
        {
            var validationResult = await _validator.ValidateAsync(genreDto);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException<GenreDto>("GenreDto is not valid.", genreDto);
            }

            var genre = _mapper.Map<Genre>(genreDto);
            var addedGenre = await _genreRepository.Add(genre);
            return _mapper.Map<GenreDto>(addedGenre);
        }

        public async Task Update(GenreDto genreDto)
        {
            var validationResult = await _validator.ValidateAsync(genreDto);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException<GenreDto>("GenreDto is not valid.", genreDto);
            }

            var genre = _mapper.Map<Genre>(genreDto);
            await _genreRepository.Update(genre);
        }

        public async Task Delete(int id)
        {
            await _genreRepository.Delete(id);
        }
    }
}