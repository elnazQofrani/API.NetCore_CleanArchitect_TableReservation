using Domain.Entities;
using AutoMapper;
using Application.DTOs;

namespace Application.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() {

            CreateMap<Seat, SeatsDto>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Customer, CustomerAddDto>().ReverseMap();

        }
    }
}
