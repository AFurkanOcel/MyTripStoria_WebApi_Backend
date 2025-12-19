using Contracts.CityDtos;
using Contracts.CountryDtos;
using Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<CountryDto>> GetAllCountriesAsync()
        {
            var countries = await _countryRepository.GetAllCountryAsync();

            return countries.Select(c => new CountryDto
            {
                Id = c.Id,
                Name = c.Name,
                Cities = c.Cities.Select(city => new CityForCountryDto
                {
                    Id = city.Id,
                    Name = city.Name,
                }).ToList()
            }).ToList();
        }

        public async Task<CountryDto?> GetCountryByIdAsync(int id)
        {
            var country = await _countryRepository.GetCountryByIdAsync(id);
            if (country == null) return null;

            return new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
                Cities = country.Cities.Select(city => new CityForCountryDto
                {
                    Id = city.Id,
                    Name = city.Name,
                }).ToList()
            };
        }

        public async Task<Country?> GetCountryByNameAsync(string name)
        {
            return await _countryRepository.GetByNameAsync(name);
        }

        public async Task AddCountryAsync(Country country)
        {
            await _countryRepository.AddAsync(country);
        }

        public async Task<Country> UpdateCountryAsync(Country country)
        {
            return await _countryRepository.UpdateAsync(country);
        }

        public async Task DeleteCountryAsync(int countryId)
        {
            await _countryRepository.DeleteAsync(countryId);
        }
    }
}

