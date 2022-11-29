using FluentValidation;
using TechnoMarket.Services.Catalog.Dtos;

namespace TechnoMarket.Services.Catalog.Validations
{
    public class ProductUpdateDtoValidator:AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");




        }
    }
}
