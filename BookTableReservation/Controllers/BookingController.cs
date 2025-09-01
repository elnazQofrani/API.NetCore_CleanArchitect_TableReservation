using AutoMapper;
using Azure.Core;
using Domain.Entities;
using Domain.Enums;
using Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookTableReservation.CustomActionValidation;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Application.Interface;
using Application.Services;
using System.Runtime.InteropServices.Marshalling;


namespace BookTableReservation.Controllers
{
    [Authorize (Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper mapper;
        private readonly ICustomerService _customerService;
        private readonly ISeatService _seatService;
        public BookingController(IBookingService _bookingService, IMapper mapper, ICustomerService _customerService, ISeatService _seatService)
        {
            this._bookingService = _bookingService;
            this.mapper = mapper;
            this._customerService = _customerService;
            this._seatService = _seatService;
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] BookingDto bookingDto)
        {

            var customer = await _customerService.GetCustomerByIdAsync(bookingDto.CustomerId);
            if (customer == null)
                return NotFound("Customer not found.");

            var seat = await _seatService.GetSeatById(bookingDto.SeatId);
            if (seat == null)
                return NotFound("Seat not found.");

            var isSeatAvailable = await _seatService.IsSeatsAvailable(bookingDto.SeatId, bookingDto.BookingDateTime, bookingDto.StartTime);
            if (!isSeatAvailable)
                return BadRequest("Seat is pre booked");

            var booking = mapper.Map<Booking>(bookingDto);
            booking.Status = BookingStatus.Confirmed;
            var result = await _bookingService.Create(booking);
            return Ok(new { Message = "Seat booked successfully", BookingId = result.Id });
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetBookingById([FromRoute] int id)
        {
            var booking = await _bookingService.GetById(id);
            if (booking == null)
            {
                return NotFound("Booking not found");
            }

            var bookingDto = mapper.Map<BookingDto>(booking);
            return Ok(bookingDto);
        }


        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingDto bookingDto)
        {
            if (bookingDto == null)
            {
                return BadRequest("Invalid request.");
            }

            bool result = await _bookingService.Update(bookingDto, id);
            if (!result)
            {
                return Ok(new { Message = "Booking does not exist", BookingId = id });
            }
            return Ok(new { Message = "Booking updated successfully", BookingId = id });
        }


        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> CancelBooking([FromRoute] int id)
        {

            OperationResult resul = await _bookingService.Update(id);
            return Ok(resul);

        }


    }

}

