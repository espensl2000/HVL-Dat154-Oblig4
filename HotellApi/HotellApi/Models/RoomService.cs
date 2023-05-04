using System;
using System.Collections.Generic;

namespace HotellApi.Models;

public partial class RoomService
{
    public int RoomServiceId { get; set; }

    public string ServiceType { get; set; } = null!;

    public string RequestStatus { get; set; } = null!;

    public string? RequestNotes { get; set; }

    public DateTime CreatedAt { get; set; }

    public int RoomNumber { get; set; }

    public virtual Room RoomNumberNavigation { get; set; } = null!;
}
