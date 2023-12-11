using BookingAppApi.Data;

namespace BookingAppApi.DTO
{
    public class ReservationCreateDTO
    {
        public int? UserId { get; set; }
        public int? CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
