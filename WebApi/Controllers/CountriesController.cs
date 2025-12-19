using Contracts.CountryDtos;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var country = await _countryService.GetCountryByIdAsync(id);
            if (country == null)
                return NotFound($"The country with ID {id} was not found.");
            return Ok(country);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CountryPostDto countryPostDto)
        {
            // DTO -> Entity
            var country = new Country
            {
                Name = countryPostDto.Name
            };

            var countryForNameControl = await _countryService.GetCountryByNameAsync(countryPostDto.Name);
            if (countryForNameControl != null)
                return Conflict($"A country with the name:'{countryPostDto.Name}' already exists.");

            await _countryService.AddCountryAsync(country);
            return CreatedAtAction(nameof(GetById), new { id = country.Id }, countryPostDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CountryPutDto countryPutDto)
        {
            var countryForControl = await _countryService.GetCountryByIdAsync(id);
            if (countryForControl == null)
                return NotFound($"The country with ID {id} was not found.");

            // DTO -> Entity
            var country = new Country
            {
                Id = id,
                Name = countryPutDto.Name,
            };

            var countryForNameControl = await _countryService.GetCountryByNameAsync(countryPutDto.Name);
            if (countryForNameControl != null)
                return Conflict($"A country with the name:'{countryPutDto.Name}' already exists.");

            var updatedCountry = await _countryService.UpdateCountryAsync(country);
            return Ok(countryPutDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var country = await _countryService.GetCountryByIdAsync(id);
            if (country == null)
                return NotFound($"The country with ID {id} was not found.");

            await _countryService.DeleteCountryAsync(id);
            return Ok($"The country {country.Name} (ID:{id}) has been deleted.");
        }
    }
}
