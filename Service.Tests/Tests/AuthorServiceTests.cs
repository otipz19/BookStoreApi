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
    public class AuthorServiceTests
    {
        private IAuthorService _authorService;
        private IValidator<AuthorDto> _validator;

        [SetUp]
        public void SetUp()
        {
            var repository = new AuthorRepositoryMock();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.ConstructServicesUsing(ObjectFactory.CreateInstance);
                cfg.CreateMap<Author, AuthorDto>().ReverseMap();
            });
            var mapper = new Mapper(config);

            _validator = new AuthorDtoValidator();

            _authorService = new AuthorService(repository, mapper, _validator);
        }

        [Test]
        public async Task Add_ShouldThrowException_WhenNameIsEmpty()
        {
            var authorDto = new AuthorDto(0, "");
            var res = _validator.TestValidate(authorDto);
            res.ShouldHaveValidationErrorFor(a => a.Name);
            Assert.ThrowsAsync<BadRequestException<AuthorDto>>(() => _authorService.Add(authorDto));
        }

        [Test]
        public async Task Add_ShouldPass_WhenNameIsValid()
        {
            var authorDto = new AuthorDto (0, "Author Test");
            var newAuthorDto = await _authorService.Add(authorDto);
            Assert.AreEqual(authorDto.Name, newAuthorDto.Name);
        }
    }
}