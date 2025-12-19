using Entities;

namespace Repositories.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<List<Country?>> GetAllCountryAsync();
        Task<Country?> GetCountryByIdAsync(int id);
        Task<Country?> GetByNameAsync(string name);
    }
}
