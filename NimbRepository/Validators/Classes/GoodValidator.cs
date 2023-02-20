using FluentValidation;
using NimbRepository.Model.Storekeeper;


namespace NimbRepository.Validators.Classes
{
    public class GoodValidator : AbstractValidator<Good>
    {
        public GoodValidator()
        {


            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters");
            //.Matches("^[a-zA-Z]{2,}$").WithMessage("Invalid First Name");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required")
                .MaximumLength(50).WithMessage("Category cannot be longer than 50 characters");
                //.Matches("^[a-zA-Z0-9_.-]*$").WithMessage("Invalid category");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters");
               // .Matches("^[a-zA-Z0-9_.-]*$").WithMessage("Invalid First Name");

            RuleFor(x => x.Count)
                .GreaterThan(0).WithMessage("Count must be greater than 0");
                
            RuleFor(x => x.Rate)
                .GreaterThan(0).WithMessage("Rate must be greater than 0");

            RuleFor(x => x.PurchasePrice)
                .GreaterThan(0).WithMessage("PurchasePrice must be greater than 0");
        }
    }
}