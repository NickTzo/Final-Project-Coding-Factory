
using BookingAppApi.DTO;

namespace   BookingAppApi.Services.IServices
{
    public interface IReservationService
    {
        /// <summary>
        /// A service that get all the reservations in list
        /// </summary>
        /// <returns>A list of reservations</returns>
        Task<List<ReservationReadOnlyDTO>> GetAllReservationListAsync();

        /// <summary>
        /// Bring a reservation that we are looking base on the id that the reservatiion has in the database
        /// </summary>
        /// <param name="id">the id of the reservation</param>
        /// <returns>The reservation or not found</returns>
        Task<ReservationReadOnlyDTO> GetReservationById(int id);

        /// <summary>
        /// A service that bring a list of reservations base on the usersid
        /// </summary>
        /// <param name="userId">the id of the user that make the request</param>
        /// <returns>a list of reservation</returns>
        Task<List<ReservationReadOnlyDTO>> GetReservationByUserId(int userId);

        /// <summary>
        /// A service to create a reservation for a car
        /// </summary>
        /// <param name="request">The details for the reservation like dates</param>
        /// <returns>Rerurn ok and comfirmation number otherwise stays in the same page</returns>
        Task<ReservationReadOnlyDTO> CreateReservation(ReservationCreateDTO request);

        /// <summary>
        /// A service to update a reservation if the customer want to change somthing(for latest version)
        /// </summary>
        /// <param name="id">the id for the customer that want to make the change in his reservation</param>
        /// <param name="request">the details for the new reservation</param>
        /// <returns>Return ok and a new comfirmation number</returns>
        Task<ReservationReadOnlyDTO> UpdateReservation(int id, ReservationUpdateDTO request);

        /// <summary>
        /// A service to delete a comfirmation base the id of the reservation
        /// </summary>
        /// <param name="id">the id of the reservation tha exists in the database</param>
        /// <returns>retunr ok if deleted correctly</returns>
        Task<bool> DeleteReservation(int id);
    }
}
