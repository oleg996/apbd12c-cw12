using
cw11.DTOs;
namespace cw11.Services;

public interface IDbService
{
    Task<List<ClientDTO>> GetClients();

    Task<List<TripDTO>> GetTrips();

    Task<Boolean> DoesClientHaveTrips(int IdClient);

    Task<Boolean> DoesClientExists(int IdClient);

    Task RemoveClient(int IdClient);

    Task<Boolean> DoesClientExistsByPessel(string Pesel);

    Task<Boolean> DoesClientHaveTrips(string Pesel, int idTrip);

    Task<Boolean> IsTripFull(int IdTrip);

    Task<Boolean> IsTripValid(int IdTrip);
}