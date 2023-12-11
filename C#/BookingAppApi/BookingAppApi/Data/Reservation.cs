using System;
using System.Collections.Generic;

namespace BookingAppApi.Data;

public partial class Reservation
{
    /// <summary>
    /// The id of a reservation that the database has
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The userId how has done the reservation
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// The carid that the reservation has booked
    /// </summary>
    public int? CarId { get; set; }

    /// <summary>
    /// The date and time that start the reservation
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// The date and time that end the reservation
    /// </summary>
    public DateTime? EndDate { get; set; }

    public virtual Car? Car { get; set; }

    public virtual User? User { get; set; }
}
