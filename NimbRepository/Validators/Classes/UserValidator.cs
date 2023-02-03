﻿using FluentValidation;
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
                .Length(5, 15).WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotNull().WithMessage("Last name is required.");
            RuleFor(x => x.PatronymicName).NotNull().WithMessage("Patronymic name is required.");
            
        
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Number is required.")
                .Matches(@"^\d{10}$").WithMessage("Number must be 10 digits.");
            RuleFor(x => x.Address).NotNull().WithMessage("Address is required.");
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("EmailAddress is invalid."); ;
        }
    }
}