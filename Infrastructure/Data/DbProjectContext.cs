
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class DbProjectContext : DbContext
    {

        public DbProjectContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<Booking> Booking { get; set; }

    }

}
