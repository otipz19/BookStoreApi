using Domain.Dto;
using FluentValidation;

namespace Service.Validators;

public class AuthorDtoValidator : AbstractValidator<AuthorDto>
{
    public AuthorDtoValidator()
    {
        RuleFor(a => a.Id).GreaterThanOrEqualTo(0);
        RuleFor(a => a.Name).NotEmpty().MaximumLength(500);
    }
}