using Domain.Entities;

namespace Application.Interface
{
    public interface ISeatRepository : IRepository<Seat>
    {
        Task<List<Seat>> GetAvailableSeats(DateTime desiredDateTime, TimeSpan desireStartTime);
        Task<bool> IsSeatsAvailable( int id,  DateTime desiredDateTime, TimeSpan desireStartTime);

    }
}
