using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Domain.Enums;
using Application.Interface;



namespace Application.Services
{
    public class BookingService : IBookingService
    {   
        private readonly IMapper mapper;
      
      
        IBookingRepository bookingRepository;
        public BookingService( IBookingRepository _bookingRepository, IMapper _mapper)
        {   
          
            bookingRepository = _bookingRepository;
            mapper = _mapper;
        }

        public async Task<Booking> Create(Booking booking)
        {
                    
                await bookingRepository.Create(booking);
                await bookingRepository.Save();
                
            return booking;
        }

        public async Task<Booking?> GetById(int Id)
        {
            return await bookingRepository.GetById(Id);

        }

        public async Task<bool> Update(BookingDto bookingDto, int id)
        {
            var model = await bookingRepository.GetById(id);

            if (model == null)
                return false;

            mapper.Map(bookingDto, model);

            await bookingRepository.Save();

            return true;


        }


        public async Task<OperationResult> Update( int id)
        {
            var model = await bookingRepository.GetById(id);

            if (model == null)
                return new OperationResult{ Sucsess= false, Message = "This booking Id not found"};


            if (model.Status == BookingStatus.Canceled)
                return new OperationResult{ Sucsess = false, Message = "This booking is already canceled." };
           

            model.Status = BookingStatus.Canceled;

            await bookingRepository.Save();

            return new OperationResult
            {
                Sucsess = true,
                Message = "Booking canceled successfully",
           
            };


        }

     
    }
}
