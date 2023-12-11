using System;
using System.Collections.Generic;

namespace BookingAppApi.Data;

public partial class User
{
    /// <summary>
    /// The id of a user that is writen in the database
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The username that the user holds for the app
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// The password that the user has to login in the app
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// The firstname that the user
    /// </summary>
    public string? Firstname { get; set; }

    /// <summary>
    /// The lastname that the user
    /// </summary>
    public string? Lastname { get; set; }

    /// <summary>
    /// The email that the user 
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// The phone that the user 
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// It show if the user is a customer or a owner that rents cars (for later version perpuse)
    /// </summary>
    public bool? IsOwner { get; set; } = false;
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
