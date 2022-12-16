using FluentValidation;
using TechnoMarket.Services.Basket.Dtos;

namespace TechnoMarket.Services.Basket.Validations
{
    public class BasketDtoValidator: AbstractValidator<BasketDto>
    {
        public BasketDtoValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Length(36).WithMessage("{PropertyName} must be 36 character");

            RuleForEach(x => x.BasketItems).SetValidator(new BasketItemDtoValidator());

        }
    }
}
