using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using cw11.Controllers;
using cw11.data;
using cw11.DTOs;

namespace cw11.Services;

public class DbService : IDbService
{
    private readonly MasterContext _context;
    public DbService(MasterContext context)
    {
        _context = context;
    }


    public async Task<List<ClientDTO>> GetClients()
    {
        return await _context.Clients.Select(e => new ClientDTO() { FirstName = e.FirstName, LastName = e.LastName, Email = e.Email, Pesel = e.Pesel }).ToListAsync();
    }



    public async Task<List<TripDTO>> GetTrips()
    {

        return await _context.Trips.Select(e => new TripDTO()
        {
            Description = e.Description,
            Name = e.Name,
            DateFrom = e.DateFrom,
            DateTo = e.DateTo,
            MaxPeople = e.MaxPeople,

            Countries = _context.Countries.Join(_context.CountryTrips.Where(c => c.IdTrip == e.IdTrip), c => c.IdCountry, c => c.IdCountry, (c, ct) => new CountryDTO() { Name = c.Name }).ToList(),



            Clients = _context.Clients.Join(_context.ClientTrips.Where(c => c.IdTrip == e.IdTrip), c => c.IdClient, c => c.IdClient, (c, ct) => new ClientDTO() { FirstName = c.FirstName, LastName = c.LastName, Email = c.Email }).ToList()

        }).ToListAsync();

    }


    public async Task<Boolean> DoesClientHaveTrips(int IdClient)
    {

        return _context.ClientTrips.Where(ct => ct.IdClient == IdClient).Any();

    }

    public async Task<Boolean> DoesClientExists(int IdClient)
    {
        return _context.Clients.Where(ct => ct.IdClient == IdClient).Any();
    }

    public async Task RemoveClient(int IdClient)
    {

        Client c = _context.Clients.Where(c => IdClient == c.IdClient).First();
        _context.Clients.Remove(c);
        await _context.SaveChangesAsync();
    }


    public async Task<Boolean> DoesClientExistsByPessel(string Pesel)
    {

        return _context.Clients.Where(ct => ct.Pesel == Pesel).Any();

    }

    public async Task<Boolean> DoesClientHaveTrips(string Pesel, int idTrip)
    {
        return await _context.Clients.Where(c => c.Pesel == Pesel).Join(_context.ClientTrips.Where(ct => ct.IdTrip == idTrip), cl => cl.IdClient, ct => ct.IdClient, (cl, ct) => cl.FirstName).AnyAsync();
    }

    public async Task<Boolean> IsTripFull(int IdTrip)
    {
        Trip t = _context.Trips.Where(t => t.IdTrip == IdTrip).First();

        return t.MaxPeople <= await _context.ClientTrips.Where(tr => tr.IdTrip == IdTrip).CountAsync();


    }

    public async Task<Boolean> IsTripValid(int IdTrip)
    {
        Trip t = _context.Trips.Where(t => t.IdTrip == IdTrip).First();

        return t.DateTo > DateTime.Now;
    }

    public async Task AddTrip(int id, AddTripDTO tripDTO)
    {
        int IdCl = (await _context.Clients.Where(cl => cl.Pesel == tripDTO.Pesel).FirstAsync()).IdClient;

        var ct = new ClientTrip() { IdClient = IdCl, IdTrip = id, RegisteredAt = DateTime.Now };


        if (tripDTO.PaymentDate == DateOnly.MinValue)
        {

            ct.PaymentDate = null;
        }
        else
        {
            ct.PaymentDate = tripDTO.PaymentDate.ToDateTime(TimeOnly.MinValue);
        }
            

        _context.ClientTrips.Add(ct);


        await _context.SaveChangesAsync();
    }

}