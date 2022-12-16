using FluentValidation;
using TechnoMarket.Services.Basket.Dtos;

namespace TechnoMarket.Services.Basket.Validations
{
    public class BasketItemDtoValidator: AbstractValidator<BasketItemDto>
    {
        public BasketItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Length(36).WithMessage("{PropertyName} must be 36 character");

            RuleFor(x => x.ProductName)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must be 51 character");

            RuleFor(x => x.Price)
                .InclusiveBetween(1, decimal.MaxValue).WithMessage("{PropertyName} must be greater 0");

            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");
        }
    }
}
