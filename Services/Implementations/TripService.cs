using Contracts.TripDtos;
using Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<List<TripDto>> GetAllTripsAsync()
        {
            var trips = await _tripRepository.GetAllAsync();

            var tripDtos = new List<TripDto>();
            foreach (var trip in trips)
            {
                tripDtos.Add(new TripDto
                {
                    TripID = trip.TripID,
                    IsCompleted = trip.IsCompleted,
                    Title = trip.Title,
                    Description = trip.Description,
                    TripType = trip.TripType,
                    CountryId = trip.CountryId,
                    CityId = trip.CityId,
                    StartDate = trip.StartDate,
                    EndDate = trip.EndDate,
                    Notes = trip.Notes,
                    UserId = trip.UserID
                });
            }
            return tripDtos;
        }

        public async Task<TripDto> GetTripByIdAsync(int tripId)
        {
            var trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null)
                return null;

            return new TripDto
            {
                TripID = trip.TripID,
                IsCompleted = trip.IsCompleted,
                Title = trip.Title,
                Description = trip.Description,
                TripType = trip.TripType,
                CountryId = trip.CountryId,
                CityId = trip.CityId,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Notes = trip.Notes,
                UserId = trip.UserID
            };
        }

        public async Task<List<TripDto>> GetTripsByUserIdAsync(int userId)
        {
            var trips = await _tripRepository.FindAsync(t => t.UserID == userId);

            var tripDtos = trips.Select(trip => new TripDto
            {
                UserId = trip.UserID,
                TripID = trip.TripID,
                IsCompleted = trip.IsCompleted,
                Title = trip.Title,
                Description = trip.Description,
                TripType = trip.TripType,
                CountryId = trip.CountryId,
                CityId = trip.CityId,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Notes = trip.Notes,
            }).ToList();

            return tripDtos;
        }

        public async Task AddTripAsync(Trip trip)
        {
            await _tripRepository.AddAsync(trip);
        }

        public async Task DeleteTripAsync(int tripId)
        {
            await _tripRepository.DeleteAsync(tripId);
        }

        public async Task<Trip> UpdateTripAsync(Trip trip)
        {
            return await _tripRepository.UpdateAsync(trip);
        }
    }
}
