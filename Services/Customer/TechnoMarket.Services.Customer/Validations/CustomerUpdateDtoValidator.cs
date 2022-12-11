using FluentValidation;
using TechnoMarket.Services.Customer.Dtos;

namespace TechnoMarket.Services.Customer.Validations
{
    public class CustomerUpdateDtoValidator:AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Length(36).WithMessage("{PropertyName} must be 36 character");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(55).WithMessage("{PropertyName} must be less than 56 character");

            RuleFor(x => x.Email)
               .NotNull().WithMessage("{PropertyName} is required")
               .NotEmpty().WithMessage("{PropertyName} is required")
               .MaximumLength(255).WithMessage("{PropertyName} must be less than 256 character");

            RuleFor(x => x.Address.AddressLine)
               .NotNull().WithMessage("{PropertyName} is required")
               .NotEmpty().WithMessage("{PropertyName} is required")
               .MaximumLength(255).WithMessage("{PropertyName} must be less than 256 character");

            RuleFor(x => x.Address.City)
              .NotNull().WithMessage("{PropertyName} is required")
              .NotEmpty().WithMessage("{PropertyName} is required")
              .MaximumLength(55).WithMessage("{PropertyName} must be less than 56 character");

            RuleFor(x => x.Address.Country)
              .NotNull().WithMessage("{PropertyName} is required")
              .NotEmpty().WithMessage("{PropertyName} is required")
              .MaximumLength(55).WithMessage("{PropertyName} must be less than 56 character");

            RuleFor(x => x.Address.CityCode)
                .InclusiveBetween(1, 81).WithMessage("{PropertyName} must be between 1-81");
        }

    }
}
