using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class BookingDto
    {

        public int CustomerId { get; set; }

        public int SeatId { get; set; }

        public DateTime BookingDateTime { get; set; }

        public TimeSpan StartTime { get; set; }

    }
}
