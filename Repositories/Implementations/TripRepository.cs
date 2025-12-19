using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class TripRepository : Repository<Trip>, ITripRepository
    {
        public TripRepository(AppDbContext context) : base(context) { }

        public async Task<List<Trip>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Trips
                                 .Include(t => t.City)
                                 .Include(t => t.Country)
                                 .Where(t => t.UserID == userId)
                                 .ToListAsync();
        }
    }
}
