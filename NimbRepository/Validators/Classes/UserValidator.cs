using FluentValidation;
using NimbRepository.Model.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Validators.Classes
{
    public class UserValidator : AbstractValidator<User>
    {   
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("First name is required.")
                .MaximumLength(20).WithMessage("Name cannot be longer than 20 characters");
            RuleFor(x => x.LastName).NotNull().WithMessage("Last name is required.");
            RuleFor(x => x.PatronymicName).NotNull().WithMessage("Patronymic name is required.");
            
        
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Number is required.")
                .Length(10, 13).WithMessage("Number must be between 10 and 13 characters");
            RuleFor(x => x.Address).NotNull().WithMessage("Address is required.");
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("EmailAddress is invalid."); ;
        }
    }
}
