using System;
using System.Collections.Generic;
using cw11.data;

namespace cw11.DTOs;

public partial class TripDTO
{

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public int MaxPeople { get; set; }

    public List<CountryDTO> Countries { get; set; }

    public List<ClientDTO> Clients { get; set; }

    
}
