using Contracts.TripDtos;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trips = await _tripService.GetAllTripsAsync();
            return Ok(trips);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trip = await _tripService.GetTripByIdAsync(id);
            if (trip == null)
                return NotFound($"The trip with ID {id} was not found.");
            return Ok(trip);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var trips = await _tripService.GetTripsByUserIdAsync(userId);
            if (trips == null)
                return NotFound($"There is no trip record for the user with ID {userId}.") ;
            return Ok(trips);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TripPostDto tripPostDto)
        {
            // DTO -> Entity
            var trip = new Trip
            {
                UserID = tripPostDto.UserId,
                IsCompleted = tripPostDto.IsCompleted,
                Title = tripPostDto.Title,
                Description = tripPostDto.Description,
                TripType = tripPostDto.TripType,
                CountryId = tripPostDto.CountryId,
                CityId = tripPostDto.CityId,
                StartDate = tripPostDto.StartDate,
                EndDate = tripPostDto.EndDate,
                Notes = tripPostDto.Notes,
            };
            await _tripService.AddTripAsync(trip);
            return CreatedAtAction(nameof(GetById), new { id = trip.UserID }, tripPostDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TripPutDto tripPutDto)
        {
            var tripForControl = await _tripService.GetTripByIdAsync(id);
            if (tripForControl == null)
                return NotFound($"The trip with ID {id} was not found.");

            // DTO -> Entity
            var trip = new Trip
            {
                TripID = id,
                UserID = tripPutDto.UserId,
                IsCompleted = tripPutDto.IsCompleted,
                Title = tripPutDto.Title,
                Description = tripPutDto.Description,
                TripType = tripPutDto.TripType,
                CountryId = tripPutDto.CountryId,
                CityId = tripPutDto.CityId,
                StartDate = tripPutDto.StartDate,
                EndDate = tripPutDto.EndDate,
                Notes = tripPutDto.Notes,
            };

            var updatedTrip = await _tripService.UpdateTripAsync(trip);
            return Ok(updatedTrip);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var trip = await _tripService.GetTripByIdAsync(id);
            if (trip == null)
                return NotFound($"The trip with ID {id} was not found.");

            await _tripService.DeleteTripAsync(id);
            return Ok($"The Trip '{trip.Title}' (ID:{id}) has been deleted.");
        }
    }
}

