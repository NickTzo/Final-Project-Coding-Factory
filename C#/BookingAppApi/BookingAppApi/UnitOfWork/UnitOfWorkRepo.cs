using BookingApiApp.Repository;
using BookingAppApi.Data;
using BookingAppApi.Repository;
using BookingAppApi.Repository.IRepository;
using BookingAppApi.UnitOfWork.IUnitOfWork;

namespace BookingAppApi.UnitOfWork
{
    public class UnitOfWorkRepo : IUnitOfWorkRepo
    {
        private readonly BookingAppApiDbContext _dbContext;
        public UnitOfWorkRepo(BookingAppApiDbContext dbContext)
        {
            _dbContext = dbContext;
            Car = new CarRepo(_dbContext);
            User = new UserRepo(_dbContext);
            Reservation = new ReservationRepo(_dbContext);
        }

        public ICarRepo Car {  get; private set; }

        public IUserRepo User { get; private set; }

        public IReservationRepo Reservation { get; private set; }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
