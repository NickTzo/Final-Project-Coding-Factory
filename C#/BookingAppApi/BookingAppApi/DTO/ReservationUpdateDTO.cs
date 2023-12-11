using BookingAppApi.Data;

namespace BookingAppApi.DTO
{
    public class ReservationUpdateDTO : BaseDTO
    {
        public Car? CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
