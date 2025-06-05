using System;
using System.Collections.Generic;

namespace cw11.data;

public partial class ClientTrip
{
    public int IdClient { get; set; }

    public int IdTrip { get; set; }

    public DateTime RegisteredAt { get; set; }


    public DateTime? PaymentDate { get; set; }
}
