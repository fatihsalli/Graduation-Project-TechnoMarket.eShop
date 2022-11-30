using FluentValidation;
using TechnoMarket.Services.Catalog.Dtos;

namespace TechnoMarket.Services.Catalog.Validations
{
    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required").Length(24).WithMessage("{PropertyName} must be 24 character");
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");
            RuleFor(x => x.Price).InclusiveBetween(1, decimal.MaxValue).WithMessage("{PropertyName} must be greater 0");
        }
    }
}
