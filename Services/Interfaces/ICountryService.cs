using Contracts.CountryDtos;
using Entities;

namespace Services.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryDto>> GetAllCountriesAsync();
        Task<CountryDto> GetCountryByIdAsync(int countryId);
        Task<Country?> GetCountryByNameAsync(string name);
        Task AddCountryAsync(Country country);
        Task<Country> UpdateCountryAsync(Country country);
        Task DeleteCountryAsync(int countryId);
    }
}
