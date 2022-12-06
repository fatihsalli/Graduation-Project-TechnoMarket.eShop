using FluentValidation;
using TechnoMarket.Services.Order.Dtos;

namespace TechnoMarket.Services.Order.Validations
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Length(24).WithMessage("{PropertyName} must be 24 character");

            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");

            RuleFor(x => x.Price)
                .InclusiveBetween(1, double.MaxValue).WithMessage("{PropertyName} must be greater 0");

            RuleFor(x => x.Status)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(255).WithMessage("{PropertyName} must be less than 255 character");

            RuleFor(x => x.Product.Id)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Length(24).WithMessage("{PropertyName} must be 24 character");

            RuleFor(x => x.Product.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(255).WithMessage("{PropertyName} must be less than 255 character");

            RuleFor(x => x.Product.ImageUrl)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(255).WithMessage("{PropertyName} must be less than 255 character");

            RuleFor(x => x.Address.City)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(255).WithMessage("{PropertyName} must be less than 255 character");

            RuleFor(x => x.Address.Country)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(255).WithMessage("{PropertyName} must be less than 255 character");

            RuleFor(x => x.Address.CityCode)
                .InclusiveBetween(1, 81).WithMessage("{PropertyName} must be between 1-81");
        }
    }
}
