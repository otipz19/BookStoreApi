using AutoMapper;
using AutoMapper.Execution;
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
    public class GenreServiceTests
    {
        private IGenreService _genreService;
        private IValidator<GenreDto> _validator;

        [SetUp]
        public void SetUp()
        {
            var repository = new GenreRepositoryMock();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.ConstructServicesUsing(ObjectFactory.CreateInstance);
                cfg.CreateMap<Genre, GenreDto>().ReverseMap();
            });
            var mapper = new Mapper(config);

            _validator = new GenreDtoValidator();

            _genreService = new GenreService(repository, mapper, _validator);
        }

        [Test]
        public async Task Add_ShouldThrowException_WhenNameIsEmpty()
        {
            var genreDto = new GenreDto(0, "");
            var res = _validator.TestValidate(genreDto);
            res.ShouldHaveValidationErrorFor(g => g.Name);
            Assert.ThrowsAsync<BadRequestException<GenreDto>>(() => _genreService.Add(genreDto));
        }

        [Test]
        public async Task Add_ShouldPass_WhenNameIsValid()
        {
            var genreDto = new GenreDto (0, "Genre Test");
            var newGenreDto = await _genreService.Add(genreDto);
            Assert.AreEqual(genreDto.Name, newGenreDto.Name);
        }
    }
}