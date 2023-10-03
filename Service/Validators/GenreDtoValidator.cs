using Domain.Dto;
using FluentValidation;

namespace Service.Validators;

public class GenreDtoValidator : AbstractValidator<GenreDto>
{
    public GenreDtoValidator()
    {
        RuleFor(g => g.Id).GreaterThanOrEqualTo(0);
        RuleFor(g => g.Name).NotEmpty().MaximumLength(500);
    }
}