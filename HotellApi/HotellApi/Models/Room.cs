using System;
using System.Collections.Generic;

namespace HotellApi.Models;

public partial class Room
{
    public int RoomNumber { get; set; }

    public int Beds { get; set; }

    public string Quality { get; set; } = null!;

    public string? NeedService { get; set; }

    public string? ServiceType { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<RoomService> RoomServices { get; set; } = new List<RoomService>();
}
