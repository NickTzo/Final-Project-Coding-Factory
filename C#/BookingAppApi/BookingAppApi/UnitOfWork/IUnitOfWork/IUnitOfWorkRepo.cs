using BookingAppApi.Repository.IRepository;

namespace BookingAppApi.UnitOfWork.IUnitOfWork
{
    public interface IUnitOfWorkRepo
    {
        /// <summary>
        /// All the repositories of the cars for better management
        /// </summary>
        public ICarRepo Car { get; }

        /// <summary>
        /// All the repositories of the users for better management
        /// </summary>
        public IUserRepo User { get; }

        /// <summary>
        /// All the repositories of the reservation for better management
        /// </summary>
        public IReservationRepo Reservation { get; }
        public Task<bool> SaveAsync();
    }
}
