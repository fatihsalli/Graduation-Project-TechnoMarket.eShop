﻿using FluentValidation;
using TechnoMarket.Services.Order.Dtos;

namespace TechnoMarket.Services.Order.Validations
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Length(36).WithMessage("{PropertyName} must be 36 character");

            RuleFor(x => x.TotalPrice)
                .InclusiveBetween(1, decimal.MaxValue).WithMessage("{PropertyName} must be greater 0");

            RuleFor(x => x.Status)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must be less than 51 character");

            RuleFor(x => x.Address.AddressLine)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(255).WithMessage("{PropertyName} must be less than 256 character");

            RuleFor(x => x.Address.City)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must be less than 51 character");

            RuleFor(x => x.Address.Country)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must be less than 51 character");

            RuleFor(x => x.Address.CityCode)
                .InclusiveBetween(1, 81).WithMessage("{PropertyName} must be between 1-81");

            RuleForEach(x => x.OrderItems).SetValidator(new OrderItemDtoValidator());
        }
    }
}
