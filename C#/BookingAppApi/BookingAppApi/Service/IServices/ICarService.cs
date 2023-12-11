using BookingAppApi.DTO;

namespace BookingAppApi.Services.IServices
{
    public interface ICarService
    {
        /// <summary>
        /// The list of all cars that exist in the database
        /// </summary>
        /// <returns>A list of cars</returns>
        Task<List<CarReadOnlyDTO>> GetAllCars();

        /// <summary>
        /// A service that find a car base on carsId
        /// </summary>
        /// <param name="id">the id that the car has in the database</param>
        /// <returns>The car if exists otherwise not found</returns>
        Task<CarReadOnlyDTO> GetCarById(int id);

        /// <summary>
        /// A service that find a list of cars base on the brand
        /// </summary>
        /// <param name="brand">the brand that the car has in the database</param>
        /// <returns>The cars if exists otherwise not found</returns>
        Task<List<CarReadOnlyDTO>> GetCarByBrand(string brand);

        /// <summary>
        /// A service that find a car base on users id that owns the car
        /// </summary>
        /// <param name="userId">the id that the user has in the database</param>
        /// <returns>The car if exists otherwise not found</returns>
        Task<List<CarReadOnlyDTO>> GetCarByUserId(int? userId);

        /// <summary>
        /// A service that find the cars that is available in specific dates
        /// </summary>
        /// <param name="startDate">The date that the customer want to check-in</param>
        /// <param name="endDate">The date that the customer want to check-out</param>
        /// <returns>A list of availble cars if exists otherwise not found</returns>
        Task<List<CarReadOnlyDTO>> GetCarsByDate(DateTime startDate, DateTime endDate);

        /// <summary>
        /// A service that find the cars that is available in specific dates
        /// </summary>
        /// <param name="startDate">The date that the customer want to check-in</param>
        /// <param name="endDate">The date that the customer want to check-out</param>
        /// <returns>A list of availble cars if exists otherwise not found</returns>
        Task<List<CarReadOnlyDTO>> GetAvailiableCarsByDate(DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// A service that find the cars that is available in specific search
        /// </summary>
        /// <param name="request">The search that the cusomer want to do in a search bar</param>
        /// <returns>A list of availble cars if exists otherwise not found</returns>
        Task<List<CarReadOnlyDTO>> GetCarByBrandAndByDate(SearchDTO request);

        /// <summary>
        /// A service that find the cars that is available in specific search
        /// </summary>
        /// <param name="request">The search that the cusomer want to do in a search bar</param>
        /// <returns>A list of availble cars if exists otherwise not found</returns>
        Task<List<CarReadOnlyDTO>> GetCarBySearch(SearchDTO request);

        /// <summary>
        /// A service for the user to put his car in the database
        /// </summary>
        /// <param name="request">The details of the car</param>
        /// <returns>Return ok if the car inserted correctly</returns>
        Task<CarReadOnlyDTO> CarCreateAsync(CarCreateDTO request);

        /// <summary>
        /// A service for update the details of your car base on the id of the user
        /// </summary>
        /// <param name="id">The users id who want to update the car</param>
        /// <param name="request">The details of the updated car</param>
        /// <returns></returns>
        Task<CarReadOnlyDTO> CarUpdateAsync(int id,CarUpdateDTO request);

        /// <summary>
        /// A service for delete your car from the database
        /// </summary>
        /// <param name="id">The id from the car that you want to delete</param>
        /// <returns>Return in the list if the car has deleted correctly</returns>
        Task<bool> CarDeleteAsync(int id);
    }
}
