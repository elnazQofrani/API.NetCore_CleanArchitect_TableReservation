using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Booking
    {
         [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

  
        public int CustomerId { get; set; }

     
        public int SeatId { get; set; }

  
        public DateTime BookingDateTime { get; set; }


        public TimeSpan StartTime { get; set; }

 
        public BookingStatus Status { get; set; }

        public Customer Customer { get; set; }

        public Seat Seat { get; set; }

    }


}

