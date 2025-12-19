using Contracts.CityDtos;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        public CitiesController(ICityService cityService, ICountryService countryService)
        {
            _cityService = cityService;
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cities = await _cityService.GetAllCitiesAsync();    
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            if (city == null)
                return NotFound($"The city with ID {id} was not found.");
            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityPostDto cityPostDto)
        {
            // DTO -> Entity
            var city = new City
            {
                Name = cityPostDto.Name,
                CountryId = cityPostDto.CountryId
            };

            var cityForNameControl = await _cityService.GetCityByNameAsync(cityPostDto.Name);
            if (cityForNameControl != null)
                return Conflict($"A city with the name:'{cityPostDto.Name}' already exists.");

            var country = await _countryService.GetCountryByIdAsync(cityPostDto.CountryId);
            if (country == null)
                return BadRequest($"The country with ID {cityPostDto.CountryId} was not found.");

            await _cityService.AddCityAsync(city);
            return CreatedAtAction(nameof(GetById), new { id = city.Id }, cityPostDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CityPutDto cityPutDto)
        {
            var cityForControl = await _cityService.GetCityByIdAsync(id);
            if (cityForControl == null)
                return NotFound($"The city with ID {id} was not found.");

            // DTO -> Entity
            var city = new City
            {
                Id = id,
                Name = cityPutDto.Name,
                CountryId = cityPutDto.CountryId,
            };

            var cityForNameControl = await _cityService.GetCityByNameAsync(cityPutDto.Name);
            if (cityForNameControl != null)
                return Conflict($"A city with the name:'{cityPutDto.Name}' already exists.");

            var country = await _countryService.GetCountryByIdAsync(cityPutDto.CountryId);
            if (country == null)
                return BadRequest($"The country with ID {cityPutDto.CountryId} was not found.");

            var updatedCity = await _cityService.UpdateCityAsync(city);
            return Ok(cityPutDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            if (city == null)
                return NotFound($"The city with ID {id} was not found.");

            await _cityService.DeleteCityAsync(id);
            return Ok($"The city {city.Name} (ID:{id}) has been deleted.");
        }
    }
}
