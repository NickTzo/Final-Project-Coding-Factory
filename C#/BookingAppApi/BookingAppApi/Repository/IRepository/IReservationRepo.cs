using BookingAppApi.Data;

namespace BookingAppApi.Repository.IRepository
{
    public interface IReservationRepo : IGenericRepo<Reservation>
    {
        /// <summary>
        /// A list of reservation that the owner of a car has
        /// More specific its a list of reservation so the owner of a car can see how many has booked.
        /// </summary>
        /// <param name="userId">the onwer id that has cars</param>
        /// <returns>the list of reservations</returns>
        Task<List<Reservation>> GetReservationsByUserId(int userId);
    }
}
