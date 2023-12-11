using BookingAppApi.Data;

namespace BookingAppApi.DTO
{
    public class ReservationReadOnlyDTO : BaseDTO
    {
        public int? UserId { get; set; }
        public int? CarId { get; set; }
        public User? User { get; set; }
        public Car? Car { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
