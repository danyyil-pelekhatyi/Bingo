using BingoBongo.DAL.DTOs;

namespace BingoBongo.BLL.Services
{
    public interface IGameStateService
    {
        Task<GameStateDto> RestartGame(int gameStateId);
        Task<GameStateDto> GetGameState(int userId);
        Task<GameStateDto> Roll(int gameStateId);
    }
}