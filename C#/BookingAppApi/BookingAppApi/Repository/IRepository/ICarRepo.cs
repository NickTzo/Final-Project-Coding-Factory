using BookingAppApi.Data;
using BookingAppApi.DTO;

namespace BookingAppApi.Repository.IRepository
{
    public interface ICarRepo : IGenericRepo<Car>
    {
        /// <summary>
        /// Bring the results after a search from a customer for specific dates that the customer want to book a car
        /// </summary>
        /// <param name="startDate">The starting date that the customer want to check-in the car</param>
        /// <param name="endDate">The date that the customer want to check-out the car</param>
        /// <returns>A list of the valiable cars</returns>
        Task<List<Car>> GetAvailiableCarsByDate(DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Bring the results after a search from a customer for specific brand and dates that the customer want to book a car
        /// </summary>
        /// <param name="brand">The brand of the car that the customer wish to book</param>
        /// <param name="startDate">The starting date that the customer want to check-in the car</param>
        /// <param name="endDate">The date that the customer want to check-out the car</param>
        /// <returns>A list of the valiable cars</returns>
        Task<List<Car>> GetCarByBrandAndByDate(string? brand,DateTime? startDate, DateTime? endDate);
        
    }
}
