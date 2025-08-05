using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Azure.Core;
using Application.Interface;

namespace BookTableReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatService _seatService;
        private readonly IMapper mapper;
        public SeatController(ISeatService _seatService, IMapper mapper)
        {
            this._seatService = _seatService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{desiredDateTime:datetime}/{desiredStartTime}")]
        public async Task<IActionResult> GetAvailableSeats([FromRoute] DateTime desiredDateTime, [FromRoute] string desiredStartTime)
        {
            if (!TimeSpan.TryParse(desiredStartTime, out TimeSpan startTime))
                return BadRequest("Invalid start time format.");

            var AvailableSeats = await _seatService.GetAvailableSeats(desiredDateTime, startTime);

            if (AvailableSeats == null || !AvailableSeats.Any()) return NotFound();
            return Ok(mapper.Map<List<SeatsDto>>(AvailableSeats));
        }

    }
}
