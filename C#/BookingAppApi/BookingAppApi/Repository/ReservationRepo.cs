using BookingAppApi.Data;
using BookingAppApi.Repository;
using BookingAppApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookingApiApp.Repository
{
    public class ReservationRepo : GenericRepo<Reservation>, IReservationRepo
    {
        private readonly BookingAppApiDbContext _dbContext;
        public ReservationRepo(BookingAppApiDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Reservation>> GetReservationsByUserId(int userId)
        {
            try
            {
                List<Reservation> reservations = await _dbContext.Set<Reservation>().Where(reservations => reservations.UserId == userId).ToListAsync();
                return reservations;
            }catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
