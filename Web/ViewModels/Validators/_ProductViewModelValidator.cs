using FluentValidation;
using Web.ViewModels.CatalogViewModels;
using Web.ViewModels.OrderViewModels;

namespace Web.ViewModels.Validators
{
    public class _ProductViewModelValidator : AbstractValidator<_ProductViewModel>
    {
        public _ProductViewModelValidator()
        {
            RuleFor(x => x.Rating)
                .NotNull()
                .WithMessage("Must supply rating to post review")
                .LessThanOrEqualTo(5)
                .WithMessage("Rating must be less than or equal to 5")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Rating must be greater than or equal to 1");


            RuleFor(x => x.Comment)
                .NotNull()
                .WithMessage("Must input a review")
                .MaximumLength(1000)
                .WithMessage("Review cannot exceed 1000 characters.");    
        }
    }
}
