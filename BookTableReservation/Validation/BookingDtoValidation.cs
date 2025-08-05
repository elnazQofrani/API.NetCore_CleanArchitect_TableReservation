
using Application.DTOs;
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


namespace BookTableReservation.validation
{
    public class BookingDtoValidation : AbstractValidator<BookingDto>
    {
      public  BookingDtoValidation()
        {
            RuleFor(x => x.SeatId).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.BookingDateTime).NotEmpty();
            RuleFor(x => x.StartTime).NotEmpty();

        }
    }
}
