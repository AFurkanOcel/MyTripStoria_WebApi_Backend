using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly AppDbContext _context;

        public CountryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Country?>> GetAllCountryAsync()
        {
            return await _context.Countries
                                 .Include(c => c.Cities)
                                 .ToListAsync();
        }

        public async Task<Country?> GetCountryByIdAsync(int id)
        {
            return await _context.Countries
                                 .Include(c => c.Cities)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Country?> GetByNameAsync(string name)
        {
            return await _context.Countries
                                 .FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}

