using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly DbProjectContext dbContext;


        public BookingRepository(DbProjectContext dbContext) : base(dbContext)
        {

            this.dbContext = dbContext;
        }

    

    }
}
