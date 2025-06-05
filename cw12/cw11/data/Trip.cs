using System;
using System.Collections.Generic;

namespace cw11.data;

public partial class Trip
{
    public int IdTrip { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public int MaxPeople { get; set; }
}
