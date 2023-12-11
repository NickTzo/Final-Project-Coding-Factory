using AutoMapper;
using BookingAppApi.Data;
using BookingAppApi.DTO;

namespace BookingAppApi.Configuration
{
    public class MapperConfig : Profile
    {
        /// <summary>
        /// The mapping between data and dto
        /// </summary>
        public MapperConfig()
        {
            CreateMap<CarCreateDTO, Car>().ReverseMap();
            CreateMap<CarUpdateDTO, Car>().ReverseMap();
            CreateMap<CarReadOnlyDTO, Car>().ReverseMap();
            CreateMap<UserCreateDTO, User>().ReverseMap();
            CreateMap<UserUpdateDTO, User>().ReverseMap();
            CreateMap<UserReadOnlyDTO, User>().ReverseMap();
            CreateMap<ReservationCreateDTO,Reservation>().ReverseMap();
            CreateMap<ReservationReadOnlyDTO, Reservation>().ReverseMap();
            CreateMap<ReservationUpdateDTO, Reservation>().ReverseMap();
            

        }
    }
}
