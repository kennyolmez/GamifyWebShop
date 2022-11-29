using FluentValidation;
using Web.ViewModels.OrderViewModels;

namespace Web.ViewModels.Validators
{
    public class CheckoutViewModelValidator : AbstractValidator<CheckoutViewModel>
    {
        public CheckoutViewModelValidator()
        {
            RuleFor(x => x.EmailAddress)
                .NotNull()
                .WithMessage("Email Address is required")
                .EmailAddress()
                .MaximumLength(100)
                .WithName("Email");

            RuleFor(x => x.StreetAddress)
                .NotNull()
                .WithMessage("Street address is required")
                .Matches("[A-Öa-ö0-9\\.\\-\\s\\,]")
                .WithMessage("Must be valid format")
                .WithName("Street Address");

            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .WithMessage("Phone Number is required")
                .Matches("^07(0|2|3|6|9)\\d{7}$")
                .WithMessage("Must be valid SE phone number")
                .WithName("Phone Number");

            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage("First Name is required")
                .WithName("First Name");

            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("Last Name is required")
                .WithName("Last Name");

            RuleFor(x => x.PostalCode)
                .NotNull()
                .WithMessage("Postal code is required")
                .Matches("^\\d{3}\\s*\\d{2}$")
                .WithMessage("Must be correct format for SE postal code")
                .WithName("Postal Code");

            RuleFor(x => x.City)
                .NotNull()
                .WithMessage("City is required");


        }
    }
}
