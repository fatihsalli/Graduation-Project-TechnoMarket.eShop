using FluentValidation;
using TechnoMarket.Services.Catalog.Dtos;

namespace TechnoMarket.Services.Catalog.Validations
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(255).WithMessage("{PropertyName} must be less than 256 character");
        }
    }
}
