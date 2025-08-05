using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Domain.Enums;

namespace Domain.validation
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {

            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.FirstName).NotEmpty().MaximumLength(30);
            RuleFor(p => p.LastName).NotEmpty().MaximumLength(30);
            RuleFor(p => p.PhoneNumber).NotEmpty();

            RuleFor(p => p.PhoneNumber)
           .NotEmpty()
           .WithMessage("Phone number is required")
           .Matches(@"^\+?\d{10,15}$")
           .WithMessage("Invalid phone number format.");

            RuleFor(p => p.Email)
           .NotEmpty()
           .WithMessage("Email is required.")
           .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
           .WithMessage("Invalid email format.");
        }
    }
  
}
