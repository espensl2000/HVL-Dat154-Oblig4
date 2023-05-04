using System;
using System.Collections.Generic;

namespace HotellApi.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public DateTime CheckInTime { get; set; }

    public DateTime CheckOutTime { get; set; }

    public string? Status { get; set; }

    public int CustomerId { get; set; }

    public int RoomNumber { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Room RoomNumberNavigation { get; set; } = null!;
}
