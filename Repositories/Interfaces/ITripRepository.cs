using Entities;

namespace Repositories.Interfaces
{
    public interface ITripRepository : IRepository<Trip>
    {
        Task<List<Trip>> GetAllByUserIdAsync(int userId);
    }
}
