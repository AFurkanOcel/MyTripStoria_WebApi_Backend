using Entities;

namespace Repositories.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Task<List<City>> GetAllByCountryIdAsync(int countryId);
        Task<City?> GetByNameAsync(string name);
    }
}
