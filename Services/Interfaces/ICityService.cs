using Contracts.CityDtos;
using Entities;

namespace Services.Interfaces
{
    public interface ICityService
    {
        Task<List<CityDto>> GetAllCitiesAsync();
        Task<CityDto> GetCityByIdAsync(int cityId);
        Task<City?> GetCityByNameAsync(string name);
        Task AddCityAsync(City city);
        Task<City> UpdateCityAsync(City city);
        Task DeleteCityAsync(int cityId);
    }
}
