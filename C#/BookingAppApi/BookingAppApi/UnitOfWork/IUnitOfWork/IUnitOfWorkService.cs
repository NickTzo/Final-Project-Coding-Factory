using BookingAppApi.Services.IServices;

namespace BookingAppApi.UnitOfWork.IUnitOfWork
{
    public interface IUnitOfWorkService 
    {
        /// <summary>
        /// All the service of the users for better management
        /// </summary>
        IUserService User { get; }

        /// <summary>
        /// All the service of the cars for better management
        /// </summary>
        ICarService Car { get; }

        /// <summary>
        /// All the service of the reservations for better management
        /// </summary>
        IReservationService Reservation { get; }
    }
}
