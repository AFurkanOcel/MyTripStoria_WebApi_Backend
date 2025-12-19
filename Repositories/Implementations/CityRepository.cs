using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context) { }

        public async Task<List<City>> GetAllByCountryIdAsync(int countryId)
        {
            return await _context.Cities
                                 .Include(c => c.Country)
                                 .ToListAsync();
        }

        public async Task<City?> GetByNameAsync(string name)
        {
            return await _context.Cities
                                 .FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
