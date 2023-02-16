using FluentValidation;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using NimbRepository.Model.Admin;
using NimbRepository.Model.Storekeeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimbRepository.Validators.Classes
{
    public class GoodValidator : AbstractValidator<Good>
    {
        public GoodValidator()
        {
           

            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required")
                .MaximumLength(50).WithMessage("Category cannot be longer than 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters");

            RuleFor(x => x.Count)
                .GreaterThan(0).WithMessage("Count must be greater than 0");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}