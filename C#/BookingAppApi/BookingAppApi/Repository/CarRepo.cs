using BookingAppApi.Data;
using BookingAppApi.DTO;
using BookingAppApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace BookingAppApi.Repository
{
    public class CarRepo : GenericRepo<Car>, ICarRepo
    {
        private readonly BookingAppApiDbContext _dbContext;
        public CarRepo(BookingAppApiDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Car>> GetAvailiableCarsByDate(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                List<Car> cars = await _dbContext.Set<Car>().Where(
                car => car.Reservations.All(reservation => reservation.EndDate < startDate | reservation.StartDate > endDate) && car.IsVisible == true).ToListAsync();
                return cars;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<List<Car>> GetCarByBrandAndByDate(string? brand, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                List<Car> cars = await _dbContext.Set<Car>().Where(
                car => car.Reservations.All(reservation => reservation.EndDate < startDate | reservation.StartDate > endDate) && car.Brand == brand && car.IsVisible == true).ToListAsync();
                return cars;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
