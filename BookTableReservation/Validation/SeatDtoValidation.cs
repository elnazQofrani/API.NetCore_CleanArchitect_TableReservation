
using Application.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTableReservation.validation
{
    public class SeatDtoValidation : AbstractValidator<SeatsDto>
    {
     public   SeatDtoValidation()
        {
            RuleFor(e => e.Id).NotEmpty();
            RuleFor(e => e.SeatNumber).NotEmpty();

        }
    }
}
