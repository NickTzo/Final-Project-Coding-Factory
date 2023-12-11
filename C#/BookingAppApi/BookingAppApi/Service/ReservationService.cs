using AutoMapper;
using BookingAppApi.Data;
using BookingAppApi.DTO;
using BookingAppApi.Services.IServices;
using BookingAppApi.UnitOfWork.IUnitOfWork;

namespace BookingAppApi.Service
{
    public class ReservationService : IReservationService
    {
        public readonly IUnitOfWorkRepo _unitOfWorkRepo;
        private readonly IMapper _mapper;

        public ReservationService(IUnitOfWorkRepo unitOfWorkRepo, IMapper mapper)
        {
            _unitOfWorkRepo = unitOfWorkRepo;
            _mapper = mapper;
        }
        public async Task<ReservationReadOnlyDTO> CreateReservation(ReservationCreateDTO request)
        {
            Reservation reservation = _mapper.Map<Reservation>(request);
            Reservation reservationCreate = await _unitOfWorkRepo.Reservation.InsertAsync(reservation);
            ReservationReadOnlyDTO returnReservation =  _mapper.Map<ReservationReadOnlyDTO>(reservationCreate);
            await _unitOfWorkRepo.SaveAsync();
            return returnReservation;
        }

        public async Task<bool> DeleteReservation(int id)
        {
            await _unitOfWorkRepo.Reservation.HardDeleteAsync(id);
            bool result = await _unitOfWorkRepo.SaveAsync();
            return result;
        }

        public async Task<List<ReservationReadOnlyDTO>> GetAllReservationListAsync()
        {
            List<Reservation> reservations = await _unitOfWorkRepo.Reservation.GetAllAsync();
            List<ReservationReadOnlyDTO> reservationReads = _mapper.Map<List<ReservationReadOnlyDTO>>(reservations);
            return reservationReads;
        }

        public async Task<ReservationReadOnlyDTO> GetReservationById(int id)
        {
            Reservation reservation = await _unitOfWorkRepo.Reservation.GetByIdAsync(id);
            ReservationReadOnlyDTO reservationRead = _mapper.Map<ReservationReadOnlyDTO>(reservation);
            return reservationRead;
        }

        public async Task<List<ReservationReadOnlyDTO>> GetReservationByUserId(int userId)
        {
            List<Reservation> reservations = await _unitOfWorkRepo.Reservation.GetReservationsByUserId(userId);
            List<ReservationReadOnlyDTO> reservationReads = _mapper.Map<List<ReservationReadOnlyDTO>>(reservations);
            return reservationReads;
        }

        public async Task<ReservationReadOnlyDTO> UpdateReservation(int id, ReservationUpdateDTO request)
        {
            Reservation oldReservation = await _unitOfWorkRepo.Reservation.GetByIdAsync(id);
            if(oldReservation != null) {
                throw new ArgumentException(message: "Reservation is not valid");
            }
            Reservation newReservation = _mapper.Map<Reservation>(request);
            oldReservation = _unitOfWorkRepo.Reservation.Update(newReservation);
            await _unitOfWorkRepo.SaveAsync();
            ReservationReadOnlyDTO reservationRead = _mapper.Map<ReservationReadOnlyDTO>(oldReservation);
            return reservationRead;
        }
    }
}
