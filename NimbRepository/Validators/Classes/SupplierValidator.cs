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
            .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters")
            .Matches("^[a-zA-Z]{2,}$").WithMessage("Invalid name");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Number is required")
                .Length(10, 13).WithMessage("Number must be between 10 and 13 characters")
                .Matches("^\\d{3}[- ]?\\d{3}[- ]?\\d{4}$");

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("Invalid email address")
                .Matches("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required")
                .Matches("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$").WithMessage("Invalid address");
        }
    }
}