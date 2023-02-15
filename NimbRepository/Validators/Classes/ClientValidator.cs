using FluentValidation;
using NimbRepository.Model.Seller;
using NimbRepository.Model.Storekeeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Validators.Classes
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last  is required")
                .MaximumLength(50).WithMessage("Category cannot be longer than 50 characters");

            RuleFor(x => x.PatronymicName)
               .NotEmpty().WithMessage("Patronymic name is required.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Number is required.")
                .Length(10, 13).WithMessage("Number must be between 10 and 13 characters");

            RuleFor(x => x.EmailAddress)
                .EmailAddress().WithMessage("EmailAddress is invalid.")
                .NotEmpty().WithMessage("EmailAddress is required.");


        }
    }
}
