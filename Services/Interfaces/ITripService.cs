using Contracts.TripDtos;
using Entities;

namespace Services.Interfaces
{
    public interface ITripService
    {
        Task<List<TripDto>> GetAllTripsAsync();
        Task<TripDto> GetTripByIdAsync(int tripId);
        Task<List<TripDto>> GetTripsByUserIdAsync(int userId);
        Task AddTripAsync(Trip trip);
        Task<Trip> UpdateTripAsync(Trip trip);
        Task DeleteTripAsync(int tripId);
    }
}
