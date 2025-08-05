using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
