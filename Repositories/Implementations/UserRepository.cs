using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                                 .Include(u => u.Trips)
                                 .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                                 .Include(u => u.Trips)
                                 .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}

