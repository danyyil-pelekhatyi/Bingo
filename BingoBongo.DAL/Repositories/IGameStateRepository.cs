using BingoBongo.DAL.DTOs;
using BingoBongo.DAL.Models;

namespace BingoBongo.DAL.Repositories
{
    public interface IGameStateRepository : IRepository<GameState>
    {
        Task<GameState> GetByUserIdAsync(int userId);
    }
}