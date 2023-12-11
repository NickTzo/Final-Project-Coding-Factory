using System;
using System.Collections.Generic;

namespace BookingAppApi.Data;

public partial class Car
{
    /// <summary>
    /// The Id of a car in Database
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The userId that the car belong  in Database
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// The bool that shows if the car will be visible in a search
    /// </summary>
    public bool? IsVisible {  get; set; }

    /// <summary>
    /// The brand of a car
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// The model of a car
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// The year of a car
    /// </summary>
    public string? Year { get; set; }

    /// <summary>
    /// How many seats the car has
    /// </summary>
    public string? Seat { get; set; }

    /// <summary>
    /// The cc(ex 2000cc) of a car
    /// </summary>
    public string? Cc { get; set; }

    /// <summary>
    /// The transmission of a car
    /// </summary>
    public string? Transmission { get; set; }

    /// <summary>
    /// The price for rent per day of a car
    /// </summary>
    public double? Price { get; set; }

    /// <summary>
    /// The photoUrl of a car
    /// </summary>
    public string? PhotoUrl {  get; set; }

    /// <summary>
    /// The photoId of a car for the database in cloudinary
    /// </summary>
    public string? PhotoId {  get; set; }

    

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
