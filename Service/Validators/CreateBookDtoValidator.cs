using Domain.Dto;
using FluentValidation;

namespace Service.Validators;

public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{
    public CreateBookDtoValidator()
    {
        RuleFor(b => b.Title).NotEmpty().MaximumLength(500);
        RuleFor(b => b.Price).GreaterThan(0);
        RuleFor(b => b.Quantity).GreaterThanOrEqualTo(0);
        RuleFor(b => b.AuthorId).GreaterThan(0);
        RuleFor(b => b.GenreId).GreaterThan(0);
    }
}