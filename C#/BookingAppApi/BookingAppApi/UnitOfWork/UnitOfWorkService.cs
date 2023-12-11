using AutoMapper;
using BookingApiApp.Services;
using BookingAppApi.Helpers;
using BookingAppApi.Service;
using BookingAppApi.Services.IServices;
using BookingAppApi.UnitOfWork.IUnitOfWork;
using Microsoft.Extensions.Options;

namespace BookingAppApi.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {   
        private readonly IUnitOfWorkRepo _unitOfWorkRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UnitOfWorkService(IMapper mapper, 
            IUnitOfWorkRepo unitOfWorkRepo, 
            IConfiguration configuration, 
            IOptions<CloudinarySettings> cloudinaryConfig) 
        {
            _mapper = mapper;
            _unitOfWorkRepo = unitOfWorkRepo;
            _configuration = configuration;
            User = new UserService(_unitOfWorkRepo,_mapper,_configuration);
            Car = new CarService(_unitOfWorkRepo, _mapper, cloudinaryConfig);
            Reservation = new ReservationService(_unitOfWorkRepo, _mapper);
        }   
        public IUserService User { get; private set; }
        public ICarService Car { get; private set; }
        public IReservationService Reservation { get; private set; }
    }
}
