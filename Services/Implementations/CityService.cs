using Contracts.CityDtos;
using Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;

        public CityService(ICityRepository cityRepository, ICountryRepository countryRepository)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        public async Task<List<CityDto>> GetAllCitiesAsync()
        {
            var cities = await _cityRepository.GetAllAsync();

            var cityDtos = new List<CityDto>();
            foreach (var city in cities)
            {
                var country = await _countryRepository.GetByIdAsync(city.CountryId);

                cityDtos.Add(new CityDto
                {
                    Id = city.Id,
                    Name = city.Name,
                    CountryId = city.CountryId,
                    CountryName = country?.Name
                });
            }

            return cityDtos;
        }

        public async Task<CityDto> GetCityByIdAsync(int cityId)
        {
            var city = await _cityRepository.GetByIdAsync(cityId);
            if (city == null)
                return null;

            var country = await _countryRepository.GetByIdAsync(city.CountryId);

            return new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId,
                CountryName = country?.Name
            };
        }

        public async Task<City?> GetCityByNameAsync(string name)
        {
            return await _cityRepository.GetByNameAsync(name);
        }

        public async Task AddCityAsync(City city)
        {
            await _cityRepository.AddAsync(city);
        }

        public async Task<City> UpdateCityAsync(City city)
        {
            return await _cityRepository.UpdateAsync(city);
        }

        public async Task DeleteCityAsync(int cityId)
        {
            await _cityRepository.DeleteAsync(cityId);
        }
    }
}
