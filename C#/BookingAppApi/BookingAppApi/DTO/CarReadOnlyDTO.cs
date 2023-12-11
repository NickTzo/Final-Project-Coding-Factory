namespace BookingAppApi.DTO
{
    public class CarReadOnlyDTO : BaseDTO
    {
    public int? UserId { get; set; }
    public bool? IsVisible {  get; set; }
    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? Year { get; set; }

    public string? Seat { get; set; }
    public string? Cc { get; set; }
    public string? Transmission { get; set; }
    public string? PhotoUrl {  get; set; }
    public string? PhotoId {  get; set; }
    public double? Price { get; set; }
    }
}
