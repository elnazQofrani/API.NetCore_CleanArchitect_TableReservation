using Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.validation
{
    internal class SeatValidation : AbstractValidator<Seat>
    {
        SeatValidation()
        {
            RuleFor(e => e.Id).NotEmpty();
            RuleFor(e => e.SeatNumber).NotEmpty();

        }
    }
}
