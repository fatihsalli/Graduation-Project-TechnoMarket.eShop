using FluentValidation;
using TechnoMarket.Services.Order.Dtos;

namespace TechnoMarket.Services.Order.Validations
{
    public class OrderStatusUpdateDtoValidator : AbstractValidator<OrderStatusUpdateDto>
    {
        public OrderStatusUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Length(36).WithMessage("{PropertyName} must be 36 character");

            RuleFor(x => x.Status)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must be less than 51 character");
        }
    }
}
