using Domain.Entities;

namespace Application.Interface
{
    public interface ISeatService
    {
        Task<bool> IsSeatsAvailable(int id, DateTime desiredDateTime, TimeSpan desireStartTime);

        Task<List<Seat>> GetAvailableSeats(DateTime desiredDateTime, TimeSpan desireStartTime);
        Task<Seat> GetSeatById(int id);

    }
}
