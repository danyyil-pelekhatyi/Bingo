using BingoBongo.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BingoBongo.DAL.Repositories
{
    public class GameStateRepository : Repository<GameState>, IGameStateRepository, IRepository<GameState>
    {
        private readonly DbContext _dbContext;

        public GameStateRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GameState> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Set<GameState>()
                .AsNoTracking()
                .Where(g => g.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
