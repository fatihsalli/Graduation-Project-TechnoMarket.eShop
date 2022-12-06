using FluentValidation;
using TechnoMarket.Services.Catalog.Dtos;

namespace TechnoMarket.Services.Catalog.Validations
{
    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Length(24).WithMessage("{PropertyName} must be 24 character");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(255).WithMessage("{PropertyName} must be less than 256 character");

            RuleFor(x => x.Stock)
                .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");

            RuleFor(x => x.Price)
                .InclusiveBetween(1, decimal.MaxValue).WithMessage("{PropertyName} must be greater 0");

            RuleFor(x => x.Description)
                .MaximumLength(255).WithMessage("{PropertyName} must be less than 256 character");

            RuleFor(x => x.ImageFile)
                .MaximumLength(255).WithMessage("{PropertyName} must be less than 256 character");

            RuleFor(x => x.Category.Id)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Length(24).WithMessage("{PropertyName} must be 24 character");

            RuleFor(x => x.Category.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(255).WithMessage("{PropertyName} must be less than 256 character");

            RuleFor(x => x.Feature.Color)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must be less than 51 character");

            RuleFor(x => x.Feature.Width)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must be less than 51 character");

            RuleFor(x => x.Feature.Height)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must be less than 51 character");

            RuleFor(x => x.Feature.Weight)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must be less than 51 character");
        }
    }
}
