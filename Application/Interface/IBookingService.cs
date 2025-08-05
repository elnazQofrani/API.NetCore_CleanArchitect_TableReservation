using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IBookingService
    {
        Task<Booking> Creat(Booking booking);
        Task<Booking?> GetById(int id);
        Task<bool> Update(BookingDto booking , int id );
        Task<OperationResult> Update(int id);

    }
}
