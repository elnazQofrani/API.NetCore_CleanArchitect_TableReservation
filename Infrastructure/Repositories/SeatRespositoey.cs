using Infrastructure.Data;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Application.Interface;

namespace Infrastructure.Repositories
{
    public class SeatRespositoey : Repository<Seat> , ISeatRepository
    {
        private readonly DbProjectContext dbContext;

        public SeatRespositoey(DbProjectContext dbContext): base(dbContext) 
        {
            this.dbContext = dbContext;
        }
     

        public async Task<List<Seat>> GetAvailableSeats(DateTime desiredDateTime, TimeSpan desireStartTime)
        {

            return await dbContext.Seat.Where(t => !t.Bookings
                  .Any(b =>
                      (b.BookingDateTime == desiredDateTime && b.Status == BookingStatus.Confirmed)
                          &&
                  b.StartTime == desireStartTime)
                  ).ToListAsync();

        }

        public async Task<bool> IsSeatsAvailable(int id, DateTime desiredDateTime, TimeSpan desireStartTime)
        {

            var result = await dbContext.Seat.Where(t => t.Id == id && t.Bookings
                    .Any(b =>
                        b.BookingDateTime == desiredDateTime
                        && b.Status == BookingStatus.Confirmed
                            && b.StartTime == desireStartTime)
                    ).FirstOrDefaultAsync();
            if (result == null) return true;
            return false;

        }

      
    }
}
