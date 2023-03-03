using BingoBongo.DAL.DTOs;
using BingoBongo.DAL.Models;
using BingoBongo.DAL.Repositories;

namespace BingoBongo.BLL.Services
{
    public class GameStateService : IGameStateService
    {
        private readonly IGameStateRepository _gameStateRepository;
        private readonly IUserRepository _userRepository;

        public GameStateService(IGameStateRepository gameStateRepository, IUserRepository userRepository)
        {
            _gameStateRepository = gameStateRepository;
            _userRepository = userRepository;
        }

        public async Task<GameStateDto> RestartGame(int gameStateId)
        {
            var oldGameState = await _gameStateRepository.GetByIdAsync(gameStateId);
            var gameState = await RestartGame(oldGameState);
            return new GameStateDto(gameState);
        }

        public async Task<GameStateDto> GetGameState(int userId)
        {
            GameState gameState = await _gameStateRepository.GetByUserIdAsync(userId);
            if(gameState == null)
            {
                gameState = await StartNewGame(userId);
            }
            return new GameStateDto(gameState);
        }

        public async Task<GameStateDto> Roll(int gameStateId)
        {
            var gameState = await _gameStateRepository.GetByIdAsync(gameStateId);
            if(gameState.DidWon || gameState.NumberOfRolls == 40)
            {
                // Restart the game
                gameState = await RestartGame(gameState);
            }
            else
            {
                // Roll new number
                var rnd = new Random();
                var numbers = new string(gameState.Numbers
                    .Where(c => !Char.IsWhiteSpace(c))
                    .ToArray());
                var newNumber = rnd.Next(1, 52);
                while (numbers.Contains(newNumber.ToString()))
                {
                    newNumber = rnd.Next(1, 52);
                }
                if (numbers.Length > 0)
                    gameState.Numbers = $"{numbers},{newNumber}";
                else
                    gameState.Numbers = $"{newNumber}";
                gameState.NumberOfRolls++;
                gameState.DidWon = CheckWin(gameState);
                await _gameStateRepository.UpdateAsync(gameState);
            }
            return new GameStateDto(gameState);
        }

        private string GenerateTable()
        {
            Int32[] table = new Int32[25];
            var rnd = new Random();
            for (int i = 0; i < 25; i++)
            {
                table[i] = rnd.Next(1, 52);
            }
            return string.Join(",", table);
        }

        private bool CheckWin(GameState gameState)
        {
            var table = gameState.Table.Split(',').Select(n => int.Parse(n)).ToList();
            var numbers = gameState.Numbers.Split(',').Select(n => int.Parse(n)).ToList();
            var row1 = new List<int>() { table[0], table[1], table[2], table[3], table[4] };
            var row2 = new List<int>() { table[5], table[6], table[7], table[8], table[9] };
            var row3 = new List<int>() { table[10], table[11], table[12], table[13], table[14] };
            var row4 = new List<int>() { table[15], table[16], table[17], table[18], table[19] };
            var row5 = new List<int>() { table[20], table[21], table[22], table[23], table[24] };
            var diag = new List<int>() { table[0], table[6], table[12], table[18], table[24] };
            var col1 = new List<int>() { table[0], table[5], table[10], table[15], table[20] };
            var col2 = new List<int>() { table[1], table[6], table[11], table[16], table[21] };
            var col3 = new List<int>() { table[2], table[7], table[12], table[17], table[22] };
            var col4 = new List<int>() { table[3], table[8], table[13], table[18], table[23] };
            var col5 = new List<int>() { table[4], table[9], table[14], table[19], table[24] };
            var allCombinations = new List<List<int>>() { row1, row2, row3, row4, row5, diag, col1, col2, col3, col4, col5 };
            return allCombinations.Any(set => {
                var setWins = true;
                foreach (var number in set) {
                    setWins = setWins && numbers.Contains(number);
                }
                return setWins;
            });
        }

        private async Task<GameState> StartNewGame(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var gs = GenerateNewGameState(user.Id);
            await _gameStateRepository.AddAsync(gs);
            var gameState = await _gameStateRepository.GetByUserIdAsync(userId);
            user.GameStateId = gameState.Id;
            await _userRepository.UpdateAsync(user);
            return gameState;
        }

        private async Task<GameState> RestartGame(GameState gameState)
        {
            gameState.DidWon = false;
            gameState.Numbers = "";
            gameState.NumberOfRolls = 0;
            gameState.Table = GenerateTable();
            await _gameStateRepository.UpdateAsync(gameState);

            return await _gameStateRepository.GetByIdAsync(gameState.Id);
        }

        private GameState GenerateNewGameState(int userId)
        {
            return  new GameState
            {
                UserId = userId,
                DidWon = false,
                Numbers = "",
                Table = GenerateTable()
            };
        }
    }
}
