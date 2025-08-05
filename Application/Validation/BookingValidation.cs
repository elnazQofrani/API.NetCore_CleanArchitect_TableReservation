using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.validation
{
    public class BookingValidation : AbstractValidator<Booking>
    {
        BookingValidation()
        {
            RuleFor(x => x.SeatId).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.BookingDateTime).NotEmpty();
            RuleFor(x => x.StartTime).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}
