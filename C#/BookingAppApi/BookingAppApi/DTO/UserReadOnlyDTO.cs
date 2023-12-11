namespace BookingAppApi.DTO
{
    public class UserReadOnlyDTO : BaseDTO
    {
        public string? Username { get; set; } = null!;

        public string? Firstname { get; set; } = null!;

        public string? Lastname { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }
        public string? Token { get; set; }
        public bool? IsOwner {  get; set; }
    }
}
