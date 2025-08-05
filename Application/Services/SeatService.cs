
using Application.Interface;

using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net.Http.Headers;

namespace Application.Services
{
    public class SeatService : ISeatService
    {
        
     private readonly  ISeatRepository seatRepository;

        public SeatService( ISeatRepository _seatRepository)
        {
            seatRepository = _seatRepository;
           
        }

        public async Task<Seat>GetSeatById(int id)
        {
            var seat = await seatRepository.GetById(id);
            return seat;
        }

        public async Task<List<Seat>> GetAvailableSeats(DateTime desiredDateTime, TimeSpan desireStartTime)
        {
           
            return await seatRepository.GetAvailableSeats(desiredDateTime, desireStartTime);
            
        }

        public Task<bool> IsSeatsAvailable(int id, DateTime desiredDateTime, TimeSpan desireStartTime)
        {

            return seatRepository.IsSeatsAvailable(id, desiredDateTime, desireStartTime);
        }
    }
}
