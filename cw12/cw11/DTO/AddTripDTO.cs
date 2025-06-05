using System.Collections;

namespace cw11.DTOs;

public class AddTripDTO
{

    public required string Pesel { get; set; }
    

    public DateOnly PaymentDate { get; set; }
}