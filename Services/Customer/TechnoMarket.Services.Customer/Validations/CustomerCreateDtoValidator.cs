using FluentValidation;
using TechnoMarket.Services.Customer.Dtos;

namespace TechnoMarket.Services.Customer.Validations
{
    public class CustomerCreateDtoValidator : AbstractValidator<CustomerCreateDto>
    {
        public CustomerCreateDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must be less than 51 character");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must be less than 51 character");

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
              .MaximumLength(50).WithMessage("{PropertyName} must be less than 51 character");

            RuleFor(x => x.Address.Country)
              .NotNull().WithMessage("{PropertyName} is required")
              .NotEmpty().WithMessage("{PropertyName} is required")
              .MaximumLength(50).WithMessage("{PropertyName} must be less than 51 character");

            RuleFor(x => x.Address.CityCode)
                .InclusiveBetween(1, 81).WithMessage("{PropertyName} must be between 1-81");
        }
    }
}
