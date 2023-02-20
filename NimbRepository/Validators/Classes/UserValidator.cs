using FluentValidation;
using NimbRepository.Model.Admin;

namespace NimbRepository.Validators.Classes
{
    public class UserValidator : AbstractValidator<User>
    {   
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("First name is required.")
                .MaximumLength(20).WithMessage("Name cannot be longer than 20 characters")
                .Matches("^[a-zA-Z]{2,}$").WithMessage("Invalid First name");

            RuleFor(x => x.LastName).NotNull().WithMessage("Last name is required.")
                .Matches("^[a-zA-Z]{2,}$").WithMessage("Invalid Last Name"); 

            RuleFor(x => x.PatronymicName).NotNull().WithMessage("Patronymic name is required.")
                .Matches("^[a-zA-Z]{2,}$").WithMessage("Invalid patronymic name");


            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Number is required.")
                .Length(10, 13).WithMessage("Number must be between 10 and 13 characters")
                .Matches("^\\d{3}[- ]?\\d{3}[- ]?\\d{4}$").WithMessage("Invalid Phone Number");

            RuleFor(x => x.Address).NotNull().WithMessage("Address is required.")
               .Matches("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$").WithMessage("Invalid address");

            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("EmailAddress is invalid.")
                .Matches("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")
            .WithMessage("Invalid Email Address");
        }
    }
}
