using System.Collections;

namespace cw11.DTOs;


public class TripsDTO
{
    public int PageNum { get; set; }

    public int PageSize { get; set; }

    public int allPages { get; set; }


    public List<TripDTO> trips { get; set; }


  
}