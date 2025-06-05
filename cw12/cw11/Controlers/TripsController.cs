using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cw11.Controllers;
using cw11.data;
using cw11.Services;
using Azure;
using Microsoft.IdentityModel.Tokens;
using cw11.DTOs;
namespace cw11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public TripsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetTrips(String page = "0", String PageSize = "10")
        {

            var data = await _dbService.GetTrips();

            var size = data.Count();

            data = data.Chunk(int.Parse(PageSize)).ElementAt(int.Parse(page)).ToList();





            return Ok(new TripsDTO() { PageNum = int.Parse(page), PageSize = int.Parse(PageSize), allPages = size, trips = data });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (!await _dbService.DoesClientExists(id))
                return NotFound("such client not exists");
            if (await _dbService.DoesClientHaveTrips(id))
                return BadRequest("such client has trips");



            await _dbService.RemoveClient(id);
            return Ok();

        }

        [HttpPost("{id}/Clients")]
        public async Task<IActionResult> AddTrip(int id, AddTripDTO trip)
        {
            if (!await _dbService.DoesClientExistsByPessel(trip.Pesel))
                return NotFound("such client not exists");

            if (await _dbService.DoesClientHaveTrips(trip.Pesel, id))
                return BadRequest("clien is already on this trip");
            if(await _dbService.IsTripFull(id))
                return BadRequest("trip is full");
            if (!await _dbService.IsTripValid(id))
                return BadRequest("trip has been ended");


            await _dbService.AddTrip(id, trip);

            return Created();
        }



    
    
    }
}
