using FluentValidation;
using NimbRepository.Model.Storekeeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Validators.Classes
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Number is required")
                .Length(5, 12).WithMessage("Number must be between 10 and 12 characters");

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required");
        }
    }
}